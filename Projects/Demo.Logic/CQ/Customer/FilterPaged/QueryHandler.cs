namespace Demo.Logic.CQ.Customer.FilterPaged
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.Database;
    using Common.Handlers.Interfaces;
    using Common.ValueObjects;
    using Dapper;
    using Database.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class QueryHandler : IRequestHandler<Query, Paged<Customer>>
    {
        private const string SelectQuery = @"SELECT ID, NAME, SURNAME, PHONENUMBER, ADDRESS, VERSION VERSIONINT FROM DBO.CUSTOMERS {0}";
        private const string CountQuery = @"SELECT COUNT(*) FROM DBO.CUSTOMERS";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public QueryHandler(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Paged<Customer> Handle(Query query)
        {
            var pagedFragment = CommandHelper.GetPagedFragment(query.OrderByTopSkip.TopSkip, GetSortColumns(query.OrderByTopSkip.OrderByCollection));
            var countQuery = string.Format(CountQuery);
            var selectQuery = string.Format(SelectQuery, pagedFragment.Data);
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var count = (NonNegativeInt)connection.QuerySingle<int>(countQuery);
                var select = connection.Query<Customer>(selectQuery, pagedFragment.Parameters).ToImmutableList();
                return Paged<Customer>.Create(count, select);
            }
        }

        private static NonEmptyString GetSortColumns(OrderByCollection modelOrderByCollection)
        {
            var defaultDatabaseOrderByCollection = Extensions.GetValue(() => NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"NAME", true), OrderBy.Create((NonEmptyString)"SURNAME", true) }.ToImmutableList()));
            return CommandHelper.GetTranslatedSort(modelOrderByCollection, defaultDatabaseOrderByCollection, Columns.GetMappings());
        }
    }
}

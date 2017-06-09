namespace Demo.Logic.CQ.Customer.Get
{
    using Common.Shared.TemplateMethods.Queries.Interfaces;
    using Dapper;
    using Database.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Repository : IGetRepository<Customer>
    {
        private const string SelectQuery = @"SELECT ID, NAME, SURNAME, PHONENUMBER, ADDRESS, VERSION VERSIONINT FROM DBO.CUSTOMERS WHERE ID = @ID";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<Customer> Get(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingleOrDefault<Customer>(SelectQuery, new { id = id.Value });
            }
        }
    }
}

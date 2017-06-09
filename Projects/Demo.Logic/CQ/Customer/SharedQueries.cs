namespace Demo.Logic.CQ.Customer
{
    using System;
    using Dapper;
    using Database.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class SharedQueries
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public SharedQueries(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool ExistsById(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingle<bool>("SELECT CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) FROM DBO.CUSTOMERS WHERE ID = @ID", new { id = id.Value });
            }
        }

        public Maybe<NonEmptyString> GetRowVersionById(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var result = connection.QuerySingleOrDefault<byte[]>("SELECT VERSION FROM DBO.CUSTOMERS WHERE ID = @ID", new { id = id.Value });
                return result != null ? (NonEmptyString)Convert.ToBase64String(result) : null;
            }
        }
    }
}

namespace Demo.Logic.CQ.Customer.Delete
{
    using Dapper;
    using Database.Interfaces;
    using Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        private readonly SharedQueries _sharedQueries;

        public Repository(IDbConnectionProvider dbConnectionProvider, SharedQueries sharedQueries)
        {
            _dbConnectionProvider = dbConnectionProvider;
            _sharedQueries = sharedQueries;
        }

        public bool ExistsById(PositiveInt id)
        {
            return _sharedQueries.ExistsById(id);
        }

        public Maybe<NonEmptyString> GetRowVersionById(PositiveInt id)
        {
            return _sharedQueries.GetRowVersionById(id);
        }

        public void Delete(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM DBO.CUSTOMERS WHERE ID = @ID", new { id = id.Value });
            }
        }
    }
}

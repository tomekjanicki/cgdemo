namespace Demo.Logic.CQ.Customer.Update
{
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Dapper;
    using Database.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Repository : IUpdateRepository<Command>
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

        public void Update(Command command)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("UPDATE DBO.CUSTOMERS SET NAME = @name, SURNAME = @surname, PHONENUMBER = @phoneNumber, ADDRESS = @address WHERE ID = @ID", new { id = command.IdVersion.Id.Value, name = command.Name.Value, surname = command.Surname.Value, phoneNumber = command.PhoneNumber.Value, address = command.Address.Value });
            }
        }
    }
}

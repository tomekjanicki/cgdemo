namespace Demo.Logic.CQ.Customer.Insert
{
    using Dapper;
    using Database.Interfaces;
    using Interfaces;
    using Types;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public PositiveInt Insert(Command command)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return (PositiveInt)connection.QuerySingle<int>(@"INSERT INTO DBO.CUSTOMERS (SURNAME, NAME, PHONENUMBER, ADDRESS) VALUES (@surname, @name, @phoneNumber, @address) SELECT SCOPE_IDENTITY()", new { surname = command.Surname.Value, name = command.Name.Value, phoneNumber = command.PhoneNumber.Value, address = command.Address.Value });
            }
        }
    }
}

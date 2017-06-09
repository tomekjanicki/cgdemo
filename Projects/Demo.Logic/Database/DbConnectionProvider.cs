namespace Demo.Logic.Database
{
    using System.Data;
    using Common.Database;
    using Interfaces;
    using Types;

    public sealed class DbConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetOpenDbConnection()
        {
            return DatabaseExtension.GetOpenConnection((NonEmptyString)"Main");
        }
    }
}
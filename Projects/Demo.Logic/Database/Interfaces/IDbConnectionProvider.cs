namespace Demo.Logic.Database.Interfaces
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenDbConnection();
    }
}
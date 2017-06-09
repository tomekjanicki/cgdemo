namespace Demo.Logic.CQ.Customer.Insert.Interfaces
{
    using Types;

    public interface IRepository
    {
        PositiveInt Insert(Command command);
    }
}
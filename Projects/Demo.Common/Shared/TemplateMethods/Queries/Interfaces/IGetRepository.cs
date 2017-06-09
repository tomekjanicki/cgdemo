namespace Demo.Common.Shared.TemplateMethods.Queries.Interfaces
{
    using Types;
    using Types.FunctionalExtensions;

    public interface IGetRepository<T>
        where T : class
    {
        Maybe<T> Get(PositiveInt id);
    }
}
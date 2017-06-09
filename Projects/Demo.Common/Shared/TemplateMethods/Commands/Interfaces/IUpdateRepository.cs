namespace Demo.Common.Shared.TemplateMethods.Commands.Interfaces
{
    using Types;
    using Types.FunctionalExtensions;

    public interface IUpdateRepository<in T>
    {
        bool ExistsById(PositiveInt id);

        Maybe<NonEmptyString> GetRowVersionById(PositiveInt id);

        void Update(T command);
    }
}
namespace Demo.Common.Shared.TemplateMethods.Commands.Interfaces
{
    using Types;
    using Types.FunctionalExtensions;

    public interface IDeleteRepository
    {
        bool ExistsById(PositiveInt id);

        Maybe<NonEmptyString> GetRowVersionById(PositiveInt id);

        void Delete(PositiveInt id);
    }
}
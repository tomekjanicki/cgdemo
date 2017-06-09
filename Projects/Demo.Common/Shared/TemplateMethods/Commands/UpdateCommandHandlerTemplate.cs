namespace Demo.Common.Shared.TemplateMethods.Commands
{
    using Handlers.Interfaces;
    using Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public abstract class UpdateCommandHandlerTemplate<TCommand, TUpdateRepository> : IRequestHandler<TCommand, IResult<Error>>
        where TCommand : IIdVersion, IRequest<IResult<Error>>
        where TUpdateRepository : class, IUpdateRepository<TCommand>
    {
        protected UpdateCommandHandlerTemplate(TUpdateRepository updateRepository)
        {
            UpdateRepository = updateRepository;
        }

        protected TUpdateRepository UpdateRepository { get; }

        public NonEmptyString GetRowVersionIsEmptyMessage()
        {
            return (NonEmptyString)"GetRowVersionById returned no rows";
        }

        public IResult<Error> Handle(TCommand message)
        {
            var id = message.IdVersion.Id;
            var version = message.IdVersion.Version;

            var exists = UpdateRepository.ExistsById(id);

            if (!exists)
            {
                return ((NonEmptyString)("Item with id " + id + " has not been found")).ToNotFound();
            }

            var versionFromRepository = UpdateRepository.GetRowVersionById(id);

            if (versionFromRepository.HasValue)
            {
                if (versionFromRepository.Value != version)
                {
                    return ((NonEmptyString)("Item with id " + id + " has diffrent version in store")).ToRowVersionMismatch();
                }
            }
            else
            {
                return GetRowVersionIsEmptyMessage().ToGeneric();
            }

            var result = BeforeUpdate(message);

            return result.IsFailure ? result.Error.ToGeneric() : Result<Error>.Ok().Tee(r => UpdateRepository.Update(message));
        }

        protected virtual IResult<NonEmptyString> BeforeUpdate(TCommand message)
        {
            return Result<NonEmptyString>.Ok();
        }
    }
}

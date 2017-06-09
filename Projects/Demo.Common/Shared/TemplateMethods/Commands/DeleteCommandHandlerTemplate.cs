namespace Demo.Common.Shared.TemplateMethods.Commands
{
    using Handlers.Interfaces;
    using Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public abstract class DeleteCommandHandlerTemplate<TCommand, TDeleteRepository> : IRequestHandler<TCommand, IResult<Error>>
        where TCommand : IIdVersion, IRequest<IResult<Error>>
        where TDeleteRepository : class, IDeleteRepository
    {
        protected DeleteCommandHandlerTemplate(TDeleteRepository deleteRepository)
        {
            DeleteRepository = deleteRepository;
        }

        protected TDeleteRepository DeleteRepository { get; }

        public NonEmptyString GetRowVersionIsEmptyMessage()
        {
            return (NonEmptyString)"GetRowVersionById returned no rows";
        }

        public IResult<Error> Handle(TCommand message)
        {
            var id = message.IdVersion.Id;
            var version = message.IdVersion.Version;

            var exists = DeleteRepository.ExistsById(id);

            if (!exists)
            {
                return ((NonEmptyString)("Item with id " + id + " has not been found")).ToNotFound();
            }

            var versionFromRepository = DeleteRepository.GetRowVersionById(id);

            if (versionFromRepository.HasValue)
            {
                if (versionFromRepository.Value != version)
                {
                    return ((NonEmptyString)("Item with id " + id + " has diffrent version in store")).ToRowVersionMismatch();
                }
            }
            else
            {
                return ((NonEmptyString)"GetRowVersionById returned no rows").ToGeneric();
            }

            var result = BeforeDelete(message);

            return result.IsFailure ? result.Error.ToGeneric() : Result<Error>.Ok().Tee(r => DeleteRepository.Delete(id));
        }

        protected virtual IResult<NonEmptyString> BeforeDelete(TCommand message)
        {
            return Result<NonEmptyString>.Ok();
        }
    }
}

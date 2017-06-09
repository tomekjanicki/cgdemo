namespace Demo.Common.Shared.TemplateMethods.Queries
{
    using Handlers.Interfaces;
    using Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public class GetCommandHandlerTemplate<TQuery, TGetRepository, TItem> : IRequestHandler<TQuery, IResult<TItem, Error>>
        where TQuery : IId, IRequest<IResult<TItem, Error>>
        where TGetRepository : class, IGetRepository<TItem>
        where TItem : class
    {
        protected GetCommandHandlerTemplate(TGetRepository getRepository)
        {
            GetRepository = getRepository;
        }

        protected TGetRepository GetRepository { get; }

        public IResult<TItem, Error> Handle(TQuery message)
        {
            var data = GetRepository.Get(message.Id);

            return data.HasNoValue ? ((NonEmptyString)("Item with id " + message.Id + " has not been found")).ToNotFound<TItem>() : Result<TItem, Error>.Ok(data.Value);
        }
    }
}

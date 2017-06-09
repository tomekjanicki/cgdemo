namespace Demo.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Common.Tools.Interfaces;
    using CQ.Version.Get;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class VersionGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEntryAssemblyProvider _entryAssemblyProvider;

        public VersionGetFacade(IMediator mediator, IMapper mapper, IEntryAssemblyProvider entryAssemblyProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _entryAssemblyProvider = entryAssemblyProvider;
        }

        public IResult<string, Error> Get()
        {
            var query = Query.Create(_entryAssemblyProvider.GetAssembly());

            return Helper.GetItemSimple<string, Query, NonEmptyString>(_mediator, _mapper, query);
        }
    }
}

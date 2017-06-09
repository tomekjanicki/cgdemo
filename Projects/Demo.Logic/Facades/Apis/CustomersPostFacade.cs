namespace Demo.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Customer.Insert;
    using Types;
    using Types.FunctionalExtensions;
    using WebApi.Dtos.Apis.Customer.Post;

    public sealed class CustomersPostFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersPostFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<int, Error> Post(RequestCustomer requestCustomer)
        {
            var commandResult = Command.TryCreate(requestCustomer.Name, requestCustomer.Surname, requestCustomer.PhoneNumber, requestCustomer.Address);

            return Helper.Insert<int, Command, PositiveInt>(_mediator, _mapper, commandResult);
        }
    }
}
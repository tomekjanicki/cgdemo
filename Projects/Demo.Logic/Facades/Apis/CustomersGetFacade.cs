namespace Demo.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Customer.Get;
    using Types.FunctionalExtensions;
    using WebApi.Dtos.Apis.Customer.Get;

    public sealed class CustomersGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<ResponseCustomer, Error> Get(int id)
        {
            var queryResult = Query.TryCreate(id);

            return Helper.GetItem<ResponseCustomer, Query, Customer>(_mediator, _mapper, queryResult);
        }
    }
}
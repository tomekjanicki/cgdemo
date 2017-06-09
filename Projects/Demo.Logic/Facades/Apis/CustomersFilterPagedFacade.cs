namespace Demo.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Dtos;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Customer.FilterPaged;
    using Types.FunctionalExtensions;
    using WebApi.Dtos.Apis.Customer.FilterPaged;

    public sealed class CustomersFilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersFilterPagedFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<Paged<ResponseCustomer>, Error> FilterPaged(int skip, int top, string orderBy)
        {
            var queryResult = Query.TryCreate(orderBy, skip, top);

            return Helper.GetItems<Paged<ResponseCustomer>, Query, Common.ValueObjects.Paged<Customer>>(_mediator, _mapper, queryResult);
        }
    }
}
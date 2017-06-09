namespace Demo.Logic.Facades.Apis
{
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Customer.Update;
    using Types.FunctionalExtensions;
    using WebApi.Dtos.Apis.Customer.Put;

    public sealed class CustomersPutFacade
    {
        private readonly IMediator _mediator;

        public CustomersPutFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Put(int id, RequestCustomer requestCustomer)
        {
            var commandResult = Command.TryCreate(id, requestCustomer.Version, requestCustomer.Name, requestCustomer.Surname, requestCustomer.PhoneNumber, requestCustomer.Address);

            return Helper.Update(_mediator, commandResult);
        }
    }
}
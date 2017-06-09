namespace Demo.Logic.Facades.Apis
{
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Customer.Delete;
    using Types.FunctionalExtensions;

    public sealed class CustomersDeleteFacade
    {
        private readonly IMediator _mediator;

        public CustomersDeleteFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Delete(int id, string version)
        {
            var commandResult = Command.TryCreate(id, version);

            return Helper.Delete(_mediator, commandResult);
        }
    }
}

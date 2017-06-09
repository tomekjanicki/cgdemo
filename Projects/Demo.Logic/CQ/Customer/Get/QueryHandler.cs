namespace Demo.Logic.CQ.Customer.Get
{
    using Common.Shared.TemplateMethods.Queries;
    using Common.Shared.TemplateMethods.Queries.Interfaces;

    public sealed class QueryHandler : GetCommandHandlerTemplate<Query, IGetRepository<Customer>, Customer>
    {
        public QueryHandler(IGetRepository<Customer> repository)
            : base(repository)
        {
        }
    }
}

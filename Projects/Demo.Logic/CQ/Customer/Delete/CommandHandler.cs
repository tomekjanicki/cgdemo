namespace Demo.Logic.CQ.Customer.Delete
{
    using Common.Shared.TemplateMethods.Commands;
    using Interfaces;

    public sealed class CommandHandler : DeleteCommandHandlerTemplate<Command, IRepository>
    {
        public CommandHandler(IRepository repository)
            : base(repository)
        {
        }
    }
}

namespace Demo.Logic.CQ.Customer.Update
{
    using Common.Shared.TemplateMethods.Commands;
    using Common.Shared.TemplateMethods.Commands.Interfaces;

    public sealed class CommandHandler : UpdateCommandHandlerTemplate<Command, IUpdateRepository<Command>>
    {
        public CommandHandler(IUpdateRepository<Command> repository)
            : base(repository)
        {
        }
    }
}

namespace Demo.Logic.CQ.Version.Get
{
    using Common.Handlers.Interfaces;
    using Common.Tools.Interfaces;
    using Types;

    public sealed class QueryHandler : IRequestHandler<Query, NonEmptyString>
    {
        private readonly IAssemblyVersionProvider _assemblyVersionProvider;

        public QueryHandler(IAssemblyVersionProvider assemblyVersionProvider)
        {
            _assemblyVersionProvider = assemblyVersionProvider;
        }

        public NonEmptyString Handle(Query message)
        {
            return (NonEmptyString)_assemblyVersionProvider.Get(message.Assembly).ToString();
        }
    }
}

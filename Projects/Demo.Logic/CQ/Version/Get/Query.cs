namespace Demo.Logic.CQ.Version.Get
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reflection;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Types;

    public sealed class Query : BaseCommandQuery<Query>, IRequest<NonEmptyString>
    {
        private Query(Assembly assembly, Guid commandId)
            : base(commandId)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; }

        public static Query Create(Assembly assembly)
        {
            return Create(assembly, Guid.NewGuid());
        }

        public static Query Create(Assembly assembly, Guid commandId)
        {
            return new Query(assembly, commandId);
        }

        protected override bool EqualsCore(Query other)
        {
            return base.EqualsCore(other) && Assembly == other.Assembly;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { CommandId, Assembly }.ToImmutableList());
        }
    }
}

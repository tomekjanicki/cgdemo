namespace Demo.Logic.CQ.Customer.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Common.ValueObjects;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion, Guid commandId)
            : base(commandId)
        {
            IdVersion = idVersion;
        }

        public IdVersion IdVersion { get; }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version)
        {
            return TryCreate(id, version, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version, Guid commandId)
        {
            var result = IdVersion.TryCreate(id, version, (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Id), (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Version));
            return result.OnSuccess(() => GetOkResult(new Command(result.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && IdVersion == other.IdVersion;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { IdVersion, CommandId }.ToImmutableList());
        }
    }
}

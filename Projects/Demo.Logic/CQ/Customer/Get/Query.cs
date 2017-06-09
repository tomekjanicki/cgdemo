namespace Demo.Logic.CQ.Customer.Get
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Common.Shared.TemplateMethods.Queries.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Query : BaseCommandQuery<Query>, IRequest<IResult<Customer, Error>>, IId
    {
        private Query(PositiveInt id, Guid commandId)
            : base(commandId)
        {
            Id = id;
        }

        public PositiveInt Id { get; }

        public static IResult<Query, NonEmptyString> TryCreate(int id)
        {
            return TryCreate(id, Guid.NewGuid());
        }

        public static IResult<Query, NonEmptyString> TryCreate(int id, Guid commandId)
        {
            var result = PositiveInt.TryCreate(id, (NonEmptyString)nameof(Id));
            return result.OnSuccess(() => GetOkResult(new Query(result.Value, commandId)));
        }

        protected override bool EqualsCore(Query other)
        {
            return base.EqualsCore(other) && Id == other.Id;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { Id, CommandId }.ToImmutableList());
        }
    }
}

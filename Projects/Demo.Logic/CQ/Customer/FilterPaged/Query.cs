namespace Demo.Logic.CQ.Customer.FilterPaged
{
    using System;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Common.ValueObjects;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Query : BaseCommandQuery<Query>, IRequest<Paged<Customer>>
    {
        private Query(OrderByTopSkip orderByTopSkip, Guid commandId)
            : base(commandId)
        {
            OrderByTopSkip = orderByTopSkip;
        }

        public OrderByTopSkip OrderByTopSkip { get; }

        public static IResult<Query, NonEmptyString> TryCreate(string orderBy, int skip, int top)
        {
            return TryCreate(orderBy, skip, top, Guid.NewGuid());
        }

        public static IResult<Query, NonEmptyString> TryCreate(string orderBy, int skip, int top, Guid id)
        {
            var orderByParseResult = OrderByParser.TryParse(orderBy, Columns.GetAllowedColumns());

            return orderByParseResult.
                OnSuccess(orderBys => OrderByTopSkip.TryCreate(orderBys, skip, top, (NonEmptyString)nameof(TopSkip.Top), (NonEmptyString)nameof(TopSkip.Skip))).
                OnSuccess(orderByTopSkip => GetOkResult(new Query(orderByTopSkip, id)));
        }

        protected override bool EqualsCore(Query other)
        {
            return base.EqualsCore(other) && OrderByTopSkip == other.OrderByTopSkip;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { OrderByTopSkip, CommandId }.ToImmutableList());
        }
    }
}

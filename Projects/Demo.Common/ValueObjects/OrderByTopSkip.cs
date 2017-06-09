namespace Demo.Common.ValueObjects
{
    using System.Collections.Immutable;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class OrderByTopSkip : ValueObject<OrderByTopSkip>
    {
        private OrderByTopSkip(OrderByCollection orderByCollection, TopSkip topSkip)
        {
            TopSkip = topSkip;
            OrderByCollection = orderByCollection;
        }

        public OrderByCollection OrderByCollection { get; }

        public TopSkip TopSkip { get; }

        public static IResult<OrderByTopSkip, NonEmptyString> TryCreate(OrderByCollection orderByCollection, int skip, int top, NonEmptyString skipField, NonEmptyString topField)
        {
            var topSkipResult = TopSkip.TryCreate(skip, top, skipField, topField);

            return topSkipResult.OnSuccess(() => GetOkResult(new OrderByTopSkip(orderByCollection, topSkipResult.Value)));
        }

        protected override bool EqualsCore(OrderByTopSkip other)
        {
            return TopSkip == other.TopSkip && OrderByCollection == other.OrderByCollection;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { TopSkip, OrderByCollection }.ToImmutableList());
        }
    }
}
namespace Demo.Common.ValueObjects
{
    using System.Collections.Immutable;
    using System.Linq;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class OrderByCollection : ValueObject<OrderByCollection>
    {
        private OrderByCollection(ImmutableList<OrderBy> orderBys)
        {
            OrderBys = orderBys;
        }

        public ImmutableList<OrderBy> OrderBys { get; }

        public static IResult<OrderByCollection, NonEmptyString> TryCreate(ImmutableList<OrderBy> orderBys)
        {
            var groupedList = orderBys.GroupBy(by => by.Column).Select(bys => new { bys.Key, Count = bys.Count() }).Where(arg => arg.Count > 1).ToImmutableList();

            return groupedList.Count > 0 ? GetFailResult((NonEmptyString)"At least one column is duplicated in sort expression.") : GetOkResult(new OrderByCollection(orderBys));
        }

        protected override bool EqualsCore(OrderByCollection other)
        {
            return OrderBys.SequenceEqual(other.OrderBys);
        }

        protected override int GetHashCodeCore()
        {
            return OrderBys.GetHashCode();
        }
    }
}
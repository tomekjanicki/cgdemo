namespace Demo.Common.ValueObjects
{
    using System.Collections.Immutable;
    using System.Linq;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class NonEmptyOrderByCollection : ValueObject<NonEmptyOrderByCollection>
    {
        private readonly OrderByCollection _orderByCollection;

        private NonEmptyOrderByCollection(OrderByCollection orderByCollection)
        {
            _orderByCollection = orderByCollection;
        }

        public ImmutableList<OrderBy> OrderBys => _orderByCollection.OrderBys;

        public static implicit operator OrderByCollection(NonEmptyOrderByCollection value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => OrderByCollection.TryCreate(value.OrderBys));
        }

        public static IResult<NonEmptyOrderByCollection, NonEmptyString> TryCreate(ImmutableList<OrderBy> orderBys)
        {
            var result = OrderByCollection.TryCreate(orderBys);

            if (result.IsFailure)
            {
                return GetFailResult(result.Error);
            }

            return result.Value.OrderBys.Count == 0 ? GetFailResult((NonEmptyString)("At least one " + nameof(OrderBys) + " is required")) : GetOkResult(new NonEmptyOrderByCollection(result.Value));
        }

        protected override bool EqualsCore(NonEmptyOrderByCollection other)
        {
            return OrderBys.SequenceEqual(other.OrderBys);
        }

        protected override int GetHashCodeCore()
        {
            return OrderBys.GetHashCode();
        }
    }
}
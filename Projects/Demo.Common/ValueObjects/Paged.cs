namespace Demo.Common.ValueObjects
{
    using System.Collections.Immutable;
    using System.Linq;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Paged<T> : ValueObject<Paged<T>>
    {
        private Paged(NonNegativeInt count, ImmutableList<T> items)
        {
            Count = count;
            Items = items;
        }

        public NonNegativeInt Count { get; }

        public ImmutableList<T> Items { get; }

        public static Paged<T> Create(NonNegativeInt count, ImmutableList<T> items)
        {
            return new Paged<T>(count, items);
        }

        public static IResult<Paged<T>, NonEmptyString> TryCreate(int count, ImmutableList<T> items)
        {
            var countResult = NonNegativeInt.TryCreate(count, (NonEmptyString)nameof(Count));

            return countResult.OnSuccess(() => GetOkResult(Create(countResult.Value, items)));
        }

        protected override bool EqualsCore(Paged<T> other)
        {
            return Count == other.Count && Items.SequenceEqual(other.Items);
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Count, Items }.ToImmutableList());
        }
    }
}

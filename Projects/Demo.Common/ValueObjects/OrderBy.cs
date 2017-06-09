namespace Demo.Common.ValueObjects
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class OrderBy : ValueObject<OrderBy>
    {
        private OrderBy(NonEmptyString column, bool ascending)
        {
            Column = column;
            Ascending = ascending;
        }

        public NonEmptyString Column { get; }

        public bool Ascending { get; }

        public static IResult<OrderBy, NonEmptyString> TryCreate(string column, bool ascending)
        {
            var columnResult = NonEmptyString.TryCreate(column, (NonEmptyString)nameof(Column));

            return columnResult.OnSuccess(() => GetOkResult(new OrderBy(columnResult.Value, ascending)));
        }

        public static OrderBy Create(NonEmptyString column, bool ascending)
        {
            return new OrderBy(column, ascending);
        }

        protected override bool EqualsCore(OrderBy other)
        {
            return Column == other.Column && Ascending == other.Ascending;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { Column, Ascending }.ToImmutableList());
        }
    }
}

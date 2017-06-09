namespace Demo.Types
{
    using FunctionalExtensions;

    public sealed class NonNegativeDecimal : SimpleStructValueObject<NonNegativeDecimal, decimal>
    {
        private NonNegativeDecimal(decimal value)
            : base(value)
        {
        }

        public static explicit operator NonNegativeDecimal(decimal value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator decimal(NonNegativeDecimal value)
        {
            return value.Value;
        }

        public static IResult<NonNegativeDecimal, NonEmptyString> TryCreate(decimal? value, NonEmptyString field)
        {
            return TryCreateInt(value, field, v => TryCreate(v, field));
        }

        public static IResult<NonNegativeDecimal, NonEmptyString> TryCreate(decimal value, NonEmptyString field)
        {
            return TryCreateInt(value, (NonEmptyString)(field + " can't be lower than zero"), v => v >= 0, v => new NonNegativeDecimal(v));
        }
    }
}
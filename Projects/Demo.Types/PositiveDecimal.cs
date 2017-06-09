namespace Demo.Types
{
    using FunctionalExtensions;

    public sealed class PositiveDecimal : SimpleStructValueObject<PositiveDecimal, decimal>
    {
        private PositiveDecimal(decimal value)
            : base(value)
        {
        }

        public static explicit operator PositiveDecimal(decimal value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator decimal(PositiveDecimal value)
        {
            return value.Value;
        }

        public static implicit operator NonNegativeDecimal(PositiveDecimal value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonNegativeDecimal.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<PositiveDecimal, NonEmptyString> TryCreate(decimal? value, NonEmptyString field)
        {
            return TryCreateInt(value, field, v => TryCreate(v, field));
        }

        public static IResult<PositiveDecimal, NonEmptyString> TryCreate(decimal value, NonEmptyString field)
        {
            return TryCreateInt(value, (NonEmptyString)(field + " can't be less or equal to zero"), v => v > 0, v => new PositiveDecimal(v));
        }
    }
}
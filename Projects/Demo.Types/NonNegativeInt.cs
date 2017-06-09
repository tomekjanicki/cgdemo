namespace Demo.Types
{
    using FunctionalExtensions;

    public sealed class NonNegativeInt : SimpleStructValueObject<NonNegativeInt, int>
    {
        private NonNegativeInt(int value)
            : base(value)
        {
        }

        public static explicit operator NonNegativeInt(int value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator int(NonNegativeInt value)
        {
            return value.Value;
        }

        public static implicit operator NonNegativeDecimal(NonNegativeInt value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonNegativeDecimal.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<NonNegativeInt, NonEmptyString> TryCreate(int? value, NonEmptyString field)
        {
            return TryCreateInt(value, field, v => TryCreate(v, field));
        }

        public static IResult<NonNegativeInt, NonEmptyString> TryCreate(int value, NonEmptyString field)
        {
            return TryCreateInt(value, (NonEmptyString)(field + " can't be lower than zero"), v => v >= 0, v => new NonNegativeInt(v));
        }
    }
}

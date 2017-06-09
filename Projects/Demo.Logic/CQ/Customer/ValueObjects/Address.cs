namespace Demo.Logic.CQ.Customer.ValueObjects
{
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Address : SimpleClassValueObject<Address, string>
    {
        private Address(string value)
            : base(value)
        {
        }

        public static explicit operator Address(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator string(Address name)
        {
            return name.Value;
        }

        public static implicit operator NonEmptyString(Address value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonEmptyString.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<Address, NonEmptyString> TryCreate(string address, NonEmptyString field)
        {
            const int max = 100;

            return address.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new Address(address));
        }
    }
}

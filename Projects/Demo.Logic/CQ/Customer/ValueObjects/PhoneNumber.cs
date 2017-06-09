namespace Demo.Logic.CQ.Customer.ValueObjects
{
    using Types;
    using Types.FunctionalExtensions;

    public sealed class PhoneNumber : SimpleClassValueObject<PhoneNumber, string>
    {
        private PhoneNumber(string value)
            : base(value)
        {
        }

        public static explicit operator PhoneNumber(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator string(PhoneNumber name)
        {
            return name.Value;
        }

        public static implicit operator NonEmptyString(PhoneNumber value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonEmptyString.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<PhoneNumber, NonEmptyString> TryCreate(string phoneNumber, NonEmptyString field)
        {
            if (phoneNumber == string.Empty)
            {
                return GetFailResult((NonEmptyString)"{0} can't be empty", field);
            }

            const int max = 50;

            // todo: add regex validation
            return phoneNumber.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new PhoneNumber(phoneNumber));
        }
    }
}

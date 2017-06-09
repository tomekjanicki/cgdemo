namespace Demo.Logic.CQ.Customer.ValueObjects
{
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Surname : SimpleClassValueObject<Surname, string>
    {
        private Surname(string value)
            : base(value)
        {
        }

        public static explicit operator Surname(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator string(Surname surname)
        {
            return surname.Value;
        }

        public static implicit operator NonEmptyString(Surname value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonEmptyString.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<Surname, NonEmptyString> TryCreate(string surname, NonEmptyString field)
        {
            if (surname == string.Empty)
            {
                return GetFailResult((NonEmptyString)"{0} can't be empty", field);
            }

            const int max = 50;

            return surname.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new Surname(surname));
        }
    }
}
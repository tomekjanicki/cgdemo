namespace Demo.Common.ValueObjects
{
    using System.Collections.Immutable;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class TopSkip : ValueObject<TopSkip>
    {
        private TopSkip(NonNegativeInt skip, PositiveInt top)
        {
            Skip = skip;
            Top = top;
        }

        public NonNegativeInt Skip { get; }

        public PositiveInt Top { get; }

        public static IResult<TopSkip, NonEmptyString> TryCreate(int skip, int top, NonEmptyString topField, NonEmptyString skipField)
        {
            var skipResult = NonNegativeInt.TryCreate(skip, skipField);

            var topResult = PositiveInt.TryCreate(top, topField);

            var result = new IResult<NonEmptyString>[]
            {
                skipResult,
                topResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            if (result.IsFailure)
            {
                return GetFailResult(result.Error);
            }

            return topResult.Value > Const.MaxTopSize ? GetFailResult((NonEmptyString)$"{{0}} can't be greater than {Const.MaxTopSize}", topField) : GetOkResult(new TopSkip(skipResult.Value, topResult.Value));
        }

        protected override bool EqualsCore(TopSkip other)
        {
            return Skip == other.Skip && Top == other.Top;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Skip, Top }.ToImmutableList());
        }
    }
}

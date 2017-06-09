namespace Demo.Common.Test
{
    using Shouldly;
    using Types;
    using Types.FunctionalExtensions;

    public static class Helper
    {
        public static void ShouldBeEqual<T>(IResult<T, NonEmptyString> result1, IResult<T, NonEmptyString> result2)
        {
            result1.EnsureIsNotFaliure();

            result2.EnsureIsNotFaliure();

            result1.Value.ShouldBe(result2.Value);
        }

        public static void ShouldNotBeEqual<T>(IResult<T, NonEmptyString> result1, IResult<T, NonEmptyString> result2)
        {
            result1.EnsureIsNotFaliure();

            result2.EnsureIsNotFaliure();

            result1.Value.ShouldNotBe(result2.Value);
        }
    }
}

namespace Demo.Types.Tests
{
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class TypeExtensionsTests
    {
        [Test]
        public void ToMaybe_PassingNullOrCorrectValue_ShouldSucceed([Values(null, 1)] int? value)
        {
            var result = value.ToMaybe((NonEmptyString)"Value", PositiveInt.TryCreate);
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void ToMaybe_PassingIncorrectValue_ShouldFail()
        {
            var result = ((int?)0).ToMaybe((NonEmptyString)"Value", PositiveInt.TryCreate);
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void ToNullable_Null_ShouldReturnNull()
        {
            var source = (Maybe<PositiveInt>)null;

            var result = source.ToNullable<int, PositiveInt>();

            result.ShouldBeNull();
        }

        [Test]
        public void ToNullable_NonNull_ShouldReturnValue()
        {
            const int value = 5;

            var source = (Maybe<PositiveInt>)(PositiveInt)value;

            var result = source.ToNullable<int, PositiveInt>();

            result.ShouldBe(value);
        }
    }
}
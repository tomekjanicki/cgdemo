namespace Demo.Types.Tests
{
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class StringExtensionsTests
    {
        [Test]
        public void NullShouldBeReplacedWithEmptyString()
        {
            var result = ((string)null).IfNullReplaceWithEmptyString();

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void NonNullShouldBeTheSame()
        {
            const string value = "value";

            var result = value.IfNullReplaceWithEmptyString();

            result.ShouldBe(value);
        }

        [Test]
        public void MaybeNullShouldBeReplacedWithEmptyString()
        {
            var result = ((Maybe<string>)null).IfNullReplaceWithEmptyString();

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void MaybeNonNullShouldBeTheSame()
        {
            const string value = "value";

            var result = ((Maybe<string>)value).IfNullReplaceWithEmptyString();

            result.ShouldBe(value);
        }
    }
}

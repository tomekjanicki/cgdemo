namespace Demo.Types.Tests
{
    using System;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class NonNegativeDecimalTests
    {
        [Test]
        public void NonNegativeDecimalCanBeCreatedFromNonNegativeValue()
        {
            var result = NonNegativeDecimal.TryCreate(0, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void NonNegativeDecimalCannotBeCreatedFromNullValue()
        {
            var result = NonNegativeDecimal.TryCreate(null, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void NonNegativeDecimalCannotBeCreatedFromNegativeValue()
        {
            var result = NonNegativeDecimal.TryCreate(-1, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonNegativeDecimalToDecimal()
        {
            var value = Extensions.GetValue(() => NonNegativeDecimal.TryCreate(1, (NonEmptyString)"Value"));
            decimal castResult = value;
            castResult.ShouldBeOfType<decimal>();
        }

        [Test]
        public void ShouldBePossibleDoExplicitCastFromValidDecimal()
        {
            const decimal s = 5.2M;
            var res = (NonNegativeDecimal)s;
            res.Value.ShouldBe(s);
        }

        [Test]
        public void ShouldBeNotPossibleDoExplicitCastFromInvalidDecimal()
        {
            // ReSharper disable once UnusedVariable
            Action a = () => { var res = (NonNegativeDecimal)(-1.0M); };
            a.ShouldThrow<InvalidCastException>();
        }
    }
}
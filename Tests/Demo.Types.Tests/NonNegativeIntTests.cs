namespace Demo.Types.Tests
{
    using System;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class NonNegativeIntTests
    {
        [Test]
        public void NonNegativeIntCanBeCreatedFromNonNegativeValue()
        {
            var result = NonNegativeInt.TryCreate(0, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void NonNegativeIntCannotBeCreatedFromNegativeOrNullValue([Values(null, -1)] int? value)
        {
            var result = NonNegativeInt.TryCreate(value, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonNegativeIntToInt()
        {
            var value = Extensions.GetValue(() => NonNegativeInt.TryCreate(1, (NonEmptyString)"Value"));
            int castResult = value;
            castResult.ShouldBeOfType<int>();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonNegativeIntToNonNegativeDecimal()
        {
            var value = Extensions.GetValue(() => NonNegativeInt.TryCreate(1, (NonEmptyString)"Value"));
            NonNegativeDecimal castResult = value;
            castResult.ShouldBeOfType<NonNegativeDecimal>();
        }

        [Test]
        public void ShouldBePossibleDoExplicitCastFromValidInt()
        {
            const int s = 5;
            var res = (NonNegativeInt)s;
            res.Value.ShouldBe(s);
        }

        [Test]
        public void ShouldBeNotPossibleDoExplicitCastFromInvalidInt()
        {
            // ReSharper disable once UnusedVariable
            Action a = () => { var res = (NonNegativeInt)(-1); };
            a.ShouldThrow<InvalidCastException>();
        }
    }
}
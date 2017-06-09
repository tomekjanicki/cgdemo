namespace Demo.Types.Tests
{
    using System;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class PositiveDecimalTests
    {
        [Test]
        public void PositiveDecimalCanBeCreatedFromPosititiveValue()
        {
            var result = PositiveDecimal.TryCreate(1, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void PositiveDecimalCannotBeCreatedFromNullValue()
        {
            var result = PositiveDecimal.TryCreate(null, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void PositiveDecimalCannotBeCreatedFromZeroOrNegativeValue([Values(-1, 0)] decimal value)
        {
            var result = PositiveDecimal.TryCreate(value, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastPositiveDecimalToDecimal()
        {
            var value = Extensions.GetValue(() => PositiveDecimal.TryCreate(1, (NonEmptyString)"Value"));
            decimal castResult = value;
            castResult.ShouldBeOfType<decimal>();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastPositiveDecimalToNonNegativeDecimal()
        {
            var value = Extensions.GetValue(() => PositiveDecimal.TryCreate(1, (NonEmptyString)"Value"));
            NonNegativeDecimal castResult = value;
            castResult.ShouldBeOfType<NonNegativeDecimal>();
        }

        [Test]
        public void ShouldBePossibleDoExplicitCastFromValidDecimal()
        {
            const decimal s = 5.2M;
            var res = (PositiveDecimal)s;
            res.Value.ShouldBe(s);
        }

        [Test]
        public void ShouldBeNotPossibleDoExplicitCastFromInvalidDecimal()
        {
            // ReSharper disable once UnusedVariable
            Action a = () => { var res = (PositiveDecimal)0M; };
            a.ShouldThrow<InvalidCastException>();
        }
    }
}
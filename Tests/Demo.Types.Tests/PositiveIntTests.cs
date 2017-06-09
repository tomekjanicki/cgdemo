namespace Demo.Types.Tests
{
    using System;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class PositiveIntTests
    {
        [Test]
        public void PositiveIntCanBeCreatedFromPosititiveValue()
        {
            var result = PositiveInt.TryCreate(1, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void PositiveIntCannotBeCreatedFromZeroOrNegativeOrNullValue([Values(null, -1, 0)] int? value)
        {
            var result = PositiveInt.TryCreate(value, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastPositiveIntToInt()
        {
            var value = Extensions.GetValue(() => PositiveInt.TryCreate(1, (NonEmptyString)"Value"));
            int castResult = value;
            castResult.ShouldBeOfType<int>();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastPositiveIntToNonNegativeInt()
        {
            var value = Extensions.GetValue(() => PositiveInt.TryCreate(1, (NonEmptyString)"Value"));
            NonNegativeInt castResult = value;
            castResult.ShouldBeOfType<NonNegativeInt>();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastPositiveIntToPositiveDecimal()
        {
            var value = Extensions.GetValue(() => PositiveInt.TryCreate(1, (NonEmptyString)"Value"));
            PositiveDecimal castResult = value;
            castResult.ShouldBeOfType<PositiveDecimal>();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastPositiveIntToNonNegativeDecimal()
        {
            var value = Extensions.GetValue(() => PositiveInt.TryCreate(1, (NonEmptyString)"Value"));
            NonNegativeDecimal castResult = value;
            castResult.ShouldBeOfType<NonNegativeDecimal>();
        }

        [Test]
        public void ShouldBePossibleDoExplicitCastFromValidInt()
        {
            const int s = 5;
            var res = (PositiveInt)s;
            res.Value.ShouldBe(s);
        }

        [Test]
        public void ShouldBeNotPossibleDoExplicitCastFromInvalidInt()
        {
            // ReSharper disable once UnusedVariable
            Action a = () => { var res = (PositiveInt)0; };
            a.ShouldThrow<InvalidCastException>();
        }
    }
}

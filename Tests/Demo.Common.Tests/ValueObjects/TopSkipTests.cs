namespace Demo.Common.Tests.ValueObjects
{
    using Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;
    using Test;
    using Types;

    public class TopSkipTests
    {
        [Test]
        public void ValidValuesMinTop_ShouldSucceed()
        {
            var result = TopSkip.TryCreate(0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void ValidValuesMaxTop_ShouldSucceed()
        {
            var result = TopSkip.TryCreate(0, Const.MaxTopSize, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InValidTopValue_ShouldFail([Values(0, Const.MaxTopSize + 1)] int top)
        {
            var result = TopSkip.TryCreate(0, top, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void InValidSkipValue_ShouldFail()
        {
            var result = TopSkip.TryCreate(-1, 10, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoTopSkipsWithSameValueShouldBeEqual()
        {
            var r1 = TopSkip.TryCreate(0, 10, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = TopSkip.TryCreate(0, 10, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void TwoTopSkipsWithDifferentTopShouldNotBeEqual()
        {
            var r1 = TopSkip.TryCreate(0, 10, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = TopSkip.TryCreate(0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        [Test]
        public void TwoTopSkipsWithDifferentSkipShouldNotBeEqual()
        {
            var r1 = TopSkip.TryCreate(1, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = TopSkip.TryCreate(0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }
    }
}
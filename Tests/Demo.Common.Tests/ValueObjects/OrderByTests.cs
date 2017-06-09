namespace Demo.Common.Tests.ValueObjects
{
    using Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;
    using Types;

    public class OrderByTests
    {
        [Test]
        public void ValidValues_ShouldSucceed()
        {
            var result = OrderBy.TryCreate("xx", true);
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InValidColumnValue_ShouldFail()
        {
            var result = OrderBy.TryCreate(string.Empty, true);
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoOrderBysWithSameValueShouldBeEqual()
        {
            var r1 = OrderBy.Create((NonEmptyString)"Value", true);
            var r2 = OrderBy.Create((NonEmptyString)"Value", true);
            var result = r1 == r2;
            result.ShouldBeTrue();
        }

        [Test]
        public void TwoOrderBysWithDifferentOrderShouldNotBeEqual()
        {
            var r1 = OrderBy.Create((NonEmptyString)"Value", true);
            var r2 = OrderBy.Create((NonEmptyString)"Value", false);
            var result = r1 == r2;
            result.ShouldBeFalse();
        }

        [Test]
        public void TwoOrderBysWithDifferentColumnShouldNotBeEqual()
        {
            var r1 = OrderBy.Create((NonEmptyString)"ValueX", true);
            var r2 = OrderBy.Create((NonEmptyString)"Value", true);
            var result = r1 == r2;
            result.ShouldBeFalse();
        }
    }
}
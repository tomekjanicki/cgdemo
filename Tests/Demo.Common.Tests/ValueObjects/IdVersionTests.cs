namespace Demo.Common.Tests.ValueObjects
{
    using System.Collections;
    using Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;
    using Test;
    using Types;

    public class IdVersionTests
    {
        [Test]
        public void ValidValues_ShouldSucceed()
        {
            var result = IdVersion.TryCreate(1, "v", (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InValidIdValue_ShouldFail()
        {
            var result = IdVersion.TryCreate(0, "v", (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void InValidVersionValue_ShouldFail()
        {
            var result = IdVersion.TryCreate(1, string.Empty, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoIdVersionsWithSameValueShouldBeEqual()
        {
            var r1 = IdVersion.TryCreate(1, "v", (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = IdVersion.TryCreate(1, "v", (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.InequalityData))]
        public void TwoIdVersionsWithDifferentValueShouldNotBeEqual(int id1, int id2, string v1, string v2)
        {
            var r1 = IdVersion.TryCreate(id1, v1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = IdVersion.TryCreate(id2, v2, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        private class TestDataProvider
        {
            public static IEnumerable InequalityData
            {
                get
                {
                    yield return new TestCaseData(1, 2, "v", "v");
                    yield return new TestCaseData(1, 1, "v", "vx");
                }
            }
        }
    }
}
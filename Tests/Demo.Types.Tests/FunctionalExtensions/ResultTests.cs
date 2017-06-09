namespace Demo.Types.Tests.FunctionalExtensions
{
    using System;
    using System.Collections;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class ResultTests
    {
        [Test]
        public void OkShouldBehaveAsExpected()
        {
            var result = Result<NonEmptyString>.Ok();
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();

            // ReSharper disable once UnusedVariable
            Action a = () => { var z = result.Error; };
            a.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void OkWithValueShouldBehaveAsExpected()
        {
            const int val = 1;
            var result = Result<int, NonEmptyString>.Ok(val);
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(val);

            // ReSharper disable once UnusedVariable
            Action a = () => { var z = result.Error; };
            a.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void FailShouldBehaveAsExpected()
        {
            var error = (NonEmptyString)"error";
            var result = Result<NonEmptyString>.Fail(error);
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(error);
        }

        [Test]
        public void FailWithValueShouldBehaveAsExpected()
        {
            var error = (NonEmptyString)"error";
            var result = Result<int, NonEmptyString>.Fail(error);
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Error.ShouldBe(error);

            // ReSharper disable once UnusedVariable
            Action a = () => { var z = result.Value; };
            a.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.FailData))]
        public void FailShouldBeEqualOrNotEqual(string error1, string error2, bool equal)
        {
            var r1 = Result<string>.Fail(error1);
            var r2 = Result<string>.Fail(error2);

            if (equal)
            {
                r1.ShouldBe(r2);
            }
            else
            {
                r1.ShouldNotBe(r2);
            }
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.FailData))]
        public void FailWithValueShouldBeEqualOrNotEqual(string error1, string error2, bool equal)
        {
            var r1 = Result<int, string>.Fail(error1);
            var r2 = Result<int, string>.Fail(error2);

            if (equal)
            {
                r1.ShouldBe(r2);
            }
            else
            {
                r1.ShouldNotBe(r2);
            }
        }

        [Test]
        public void OkShouldEqual()
        {
            var r1 = Result<string>.Ok();
            var r2 = Result<string>.Ok();
            r1.ShouldBe(r2);
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.OkWithValueData))]
        public void OkWithValueShouldEqualOrNotEqual(int v1, int v2, bool equal)
        {
            var r1 = Result<int, string>.Ok(v1);
            var r2 = Result<int, string>.Ok(v2);

            if (equal)
            {
                r1.ShouldBe(r2);
            }
            else
            {
                r1.ShouldNotBe(r2);
            }
        }

        private class TestDataProvider
        {
            public static IEnumerable FailData
            {
                get
                {
                    yield return new TestCaseData("e", "e", true);
                    yield return new TestCaseData("e1", "e2", false);
                }
            }

            public static IEnumerable OkWithValueData
            {
                get
                {
                    yield return new TestCaseData(1, 1, true);
                    yield return new TestCaseData(1, 2, false);
                }
            }
        }
    }
}

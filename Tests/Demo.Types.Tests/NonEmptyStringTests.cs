namespace Demo.Types.Tests
{
    using System;
    using System.Collections;
    using Common.Test;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class NonEmptyStringTests
    {
        [Test]
        public void NonEmptyStringCanBeCreatedFromNonEmptyValue()
        {
            var result = NonEmptyString.TryCreate("x", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void NonEmptyStringCannotBeCreatedFromEmptyValue()
        {
            var result = NonEmptyString.TryCreate(string.Empty, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonEmptyStringToString()
        {
            var value = Extensions.GetValue(() => NonEmptyString.TryCreate("1", (NonEmptyString)"Value"));
            string castResult = value;
            castResult.ShouldBeOfType<string>();
        }

        [Test]
        public void ShouldBePossibleDoExplicitCastFromValidString()
        {
            const string s = "x";
            var res = (NonEmptyString)s;
            res.Value.ShouldBe(s);
        }

        [Test]
        public void ShouldBeNotPossibleDoExplicitCastFromInvalidString()
        {
            // ReSharper disable once UnusedVariable
            Action a = () => { var res = (NonEmptyString)string.Empty; };
            a.ShouldThrow<InvalidCastException>();
        }

        [Test]
        public void TwoNonEmptyStringsWithSameValueShouldBeEqual()
        {
            var r1 = NonEmptyString.TryCreate("v1", (NonEmptyString)"Value");
            var r2 = NonEmptyString.TryCreate("v1", (NonEmptyString)"Value");

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.InequalityData))]
        public void TwoNonEmptyStringsWithDifferentValueShouldNotBeEqual(string s1, string s2)
        {
            var r1 = NonEmptyString.TryCreate(s1, (NonEmptyString)"Value");
            var r2 = NonEmptyString.TryCreate(s2, (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        private class TestDataProvider
        {
            public static IEnumerable InequalityData
            {
                get
                {
                    yield return new TestCaseData("v1", "V1");
                    yield return new TestCaseData("v1", "v2");
                }
            }
        }
    }
}

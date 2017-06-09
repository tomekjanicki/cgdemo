namespace Demo.Types.Tests
{
    using System;
    using System.Collections;
    using Common.Test;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class NonEmptyLowerCaseStringTests
    {
        [Test]
        public void NonEmptyLowerCaseStringCanBeCreatedFromNonEmptyValue()
        {
            var result = NonEmptyLowerCaseString.TryCreate("ABC", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
            result.Value.Value.ShouldBe("abc");
        }

        [Test]
        public void NonEmptyLowerCaseStringCannotBeCreatedFromEmptyValue()
        {
            var result = NonEmptyLowerCaseString.TryCreate(string.Empty, (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeFalse();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonEmptyLowerCaseStringToString()
        {
            var value = Extensions.GetValue(() => NonEmptyLowerCaseString.TryCreate("1", (NonEmptyString)"Value"));
            string castResult = value;
            castResult.ShouldBeOfType<string>();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonEmptyLowerCaseStringToNonEmptyString()
        {
            var value = Extensions.GetValue(() => NonEmptyLowerCaseString.TryCreate("1", (NonEmptyString)"Value"));
            NonEmptyString castResult = value;
            castResult.ShouldBeOfType<NonEmptyString>();
        }

        [Test]
        public void ShouldBePossibleDoExplicitCastFromValidString()
        {
            const string s = "x";
            var res = (NonEmptyLowerCaseString)s;
            res.Value.ShouldBe(s);
        }

        [Test]
        public void ShouldBeNotPossibleDoExplicitCastFromInvalidString()
        {
            // ReSharper disable once UnusedVariable
            Action a = () => { var res = (NonEmptyLowerCaseString)string.Empty; };
            a.ShouldThrow<InvalidCastException>();
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.CorrectData))]
        public void TwoNonEmptyLowerCaseStringWithSameValueShouldBeEqual(string s1, string s2)
        {
            var r1 = NonEmptyLowerCaseString.TryCreate(s1, (NonEmptyString)"Value");
            var r2 = NonEmptyLowerCaseString.TryCreate(s2, (NonEmptyString)"Value");

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void NonEmptyLowerCaseStringsWithDifferentValueShouldNotBeEqual()
        {
            var r1 = NonEmptyLowerCaseString.TryCreate("2", (NonEmptyString)"Value");
            var r2 = NonEmptyLowerCaseString.TryCreate("1", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        private class TestDataProvider
        {
            public static IEnumerable CorrectData
            {
                get
                {
                    yield return new TestCaseData("v1", "V1");
                    yield return new TestCaseData("v1", "v1");
                }
            }
        }
    }
}
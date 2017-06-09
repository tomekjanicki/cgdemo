namespace Demo.Common.Tests.Shared
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.Shared;
    using NUnit.Framework;
    using Shouldly;
    using Types;

    public class OrderByParserTests
    {
        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.CorrectData))]
        public void CorectData_ShouldSucceed(string value, int count)
        {
            var result = OrderByParser.TryParse(value, GetAllowedColumns());

            result.IsSuccess.ShouldBeTrue();

            result.Value.OrderBys.Count.ShouldBe(count);
        }

        public void IncorectData_ShouldFail()
        {
            var result = OrderByParser.TryParse("abc", GetAllowedColumns());

            result.IsFailure.ShouldBeTrue();
        }

        private static ImmutableList<NonEmptyString> GetAllowedColumns()
        {
            return new List<NonEmptyString>
            {
                (NonEmptyString)TestDataProvider.C1,
                (NonEmptyString)TestDataProvider.C2
            }.ToImmutableList();
        }

        private class TestDataProvider
        {
            public const string C1 = "C1";

            public const string C2 = "c2";

            public static IEnumerable CorrectData
            {
                get
                {
                    yield return new TestCaseData(string.Empty, 0);
                    yield return new TestCaseData(" ", 0);
                    yield return new TestCaseData(C1, 1);
                    yield return new TestCaseData($"+{C1}", 1);
                    yield return new TestCaseData($"+ {C1}", 1);
                    yield return new TestCaseData($"-{C1}", 1);
                    yield return new TestCaseData($"-{C1},{C2}", 2);
                }
            }
        }
    }
}

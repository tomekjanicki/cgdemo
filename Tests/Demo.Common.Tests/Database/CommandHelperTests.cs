namespace Demo.Common.Tests.Database
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.Database;
    using Common.ValueObjects;
    using Dapper;
    using NUnit.Framework;
    using Shouldly;
    using Types;
    using Types.FunctionalExtensions;

    public class CommandHelperTests
    {
        [Test]
        public void GetTranslatedSort_SortEmpty_ShouldReturnDefaultSort()
        {
            const string defaultColumn = "defaultColumn";

            var sort = Extensions.GetValue(() => OrderByCollection.TryCreate(new List<OrderBy>().ToImmutableList()));

            var mappings = ImmutableDictionary<NonEmptyString, NonEmptyString>.Empty;

            var defaultSort = GetDefaultSort(defaultColumn);

            var result = CommandHelper.GetTranslatedSort(sort, defaultSort, mappings);

            result.Value.ShouldBe($"{defaultColumn.ToUpper()} ASC");
        }

        [Test]
        public void GetTranslatedSort_SortNonEmpty_ShouldReturnSort()
        {
            const string defaultColumn = "defaultColumn";

            const string sourceColumn = "sourceColumn";

            const string destinationColumn = "destinationColumn";

            var sort = Extensions.GetValue(() => OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)sourceColumn, true) }.ToImmutableList()));

            var mappings = new Dictionary<NonEmptyString, NonEmptyString> { { (NonEmptyString)sourceColumn, (NonEmptyString)destinationColumn } }.ToImmutableDictionary();

            var defaultSort = GetDefaultSort(defaultColumn);

            var result = CommandHelper.GetTranslatedSort(sort, defaultSort, mappings);

            result.Value.ShouldBe($"{destinationColumn.ToUpper()} ASC");
        }

        [Test]
        public void GetTranslatedSort_WrongMapping_ShouldReturnDefaultSort()
        {
            const string defaultColumn = "defaultColumn";

            const string sourceColumn = "sourceColumn";

            const string destinationColumn = "destinationColumn";

            var sort = Extensions.GetValue(() => OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)sourceColumn, true) }.ToImmutableList()));

            var mappings = new Dictionary<NonEmptyString, NonEmptyString> { { (NonEmptyString)(sourceColumn + "X"), (NonEmptyString)destinationColumn } }.ToImmutableDictionary();

            var defaultSort = GetDefaultSort(defaultColumn);

            var result = CommandHelper.GetTranslatedSort(sort, defaultSort, mappings);

            result.Value.ShouldBe($"{defaultColumn.ToUpper()} ASC");
        }

        [Test]
        public void GetPagedFragment_ShouldReturnExpectedValues()
        {
            var topSkip = GetValidTopSkip();

            const string sortField = "sf";

            var result = CommandHelper.GetPagedFragment(topSkip, sortField);

            result.Data.Value.ShouldBe("ORDER BY sf OFFSET @SKIP ROWS FETCH NEXT @TOP ROWS ONLY");

            result.Parameters.Get<int>(CommandHelper.TopParamName).ShouldBe(topSkip.Top.Value);

            result.Parameters.Get<int>(CommandHelper.SkipParamName).ShouldBe(topSkip.Skip.Value);
        }

        [Test]
        public void GetSort_ShouldReturnExpectedValues()
        {
            const string sortField = "sf";

            var result = CommandHelper.GetSort(sortField);

            result.ShouldBe("ORDER BY sf");
        }

        [Test]
        public void GetSort_Empty_ShouldReturnExpectedValues()
        {
            var result = CommandHelper.GetSort(string.Empty);

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void SetValues_ShouldReturnExpectedValues()
        {
            const string p1 = "P1";
            var data = (NonEmptyString)"data";
            var criteria = new List<NonEmptyString>();
            var dp = new DynamicParameters();
            var idp = new DynamicParameters();
            idp.Add(p1, 1);
            var like = new CommandHelper.DataResult(data, idp);

            CommandHelper.SetValues(criteria, dp, like);

            criteria.ShouldContain(data);

            dp.ParameterNames.ShouldContain(p1);
        }

        [Test]
        public void GetLikeCaluse_ShouldReturnExpectedValues()
        {
            var fieldName = (NonEmptyString)"FN";
            var paramName = (NonEmptyString)"PN";
            var value = (NonEmptyString)"v";

            var result = CommandHelper.GetLikeCaluse(fieldName, paramName, value);

            result.Data.Value.ShouldBe(@"FN LIKE @PN ESCAPE '\'");

            result.Parameters.Get<string>(paramName).ShouldBe($"%{value}%");
        }

        [Test]
        public void GetLikeLeftCaluse_ShouldReturnExpectedValues()
        {
            var fieldName = (NonEmptyString)"FN";
            var paramName = (NonEmptyString)"PN";
            var value = (NonEmptyString)"v";

            var result = CommandHelper.GetLikeLeftCaluse(fieldName, paramName, value);

            result.Data.Value.ShouldBe(@"FN LIKE @PN ESCAPE '\'");

            result.Parameters.Get<string>(paramName).ShouldBe($"%{value}");
        }

        [Test]
        public void GetLikeRightCaluse_ShouldReturnExpectedValues()
        {
            var fieldName = (NonEmptyString)"FN";
            var paramName = (NonEmptyString)"PN";
            var value = (NonEmptyString)"v";

            var result = CommandHelper.GetLikeRightCaluse(fieldName, paramName, value);

            result.Data.Value.ShouldBe(@"FN LIKE @PN ESCAPE '\'");

            result.Parameters.Get<string>(paramName).ShouldBe($"{value}%");
        }

        private static TopSkip GetValidTopSkip()
        {
            return Extensions.GetValue(() => TopSkip.TryCreate(20, 20, (NonEmptyString)nameof(TopSkip.Top), (NonEmptyString)nameof(TopSkip.Skip)));
        }

        private static NonEmptyOrderByCollection GetDefaultSort(string column)
        {
           return Extensions.GetValue(() => NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)column, true) }.ToImmutableList()));
        }
    }
}

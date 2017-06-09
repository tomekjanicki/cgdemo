namespace Demo.Common.Database
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Types;
    using ValueObjects;

    public static class CommandHelper
    {
        public const string TopParamName = "TOP";
        public const string SkipParamName = "SKIP";

        private enum LikeType
        {
            Full,
            Left,
            Right
        }

        public static DataResult GetPagedFragment(TopSkip topSkip, string sort)
        {
            var dp = new DynamicParameters();
            dp.Add(SkipParamName, topSkip.Skip.Value);
            dp.Add(TopParamName, topSkip.Top.Value);
            return new DataResult((NonEmptyString)$@"{GetSort(sort)} OFFSET @{SkipParamName} ROWS FETCH NEXT @{TopParamName} ROWS ONLY", dp);
        }

        public static ImmutableList<TResult> SplitExecuteAndGetMerged<TParam, TResult>(ImmutableList<TParam> input, IDbConnection connection, Func<ImmutableList<TParam>, IDbConnection, ImmutableList<TResult>> processingFunc)
        {
            return Partitioner.SplitExecuteAndGetMerged(input, p => processingFunc(p, connection), (PositiveInt)2100);
        }

        public static ImmutableList<TResult> SplitExecuteAndGetMerged<TParam, TResult>(ImmutableList<TParam> input, IDbConnection connection, Func<ImmutableList<TParam>, IDbConnection, ImmutableList<TResult>> processingFunc, PositiveInt maxOffset)
        {
            var size = (PositiveInt)(2100 - maxOffset);
            return Partitioner.SplitExecuteAndGetMerged(input, p => processingFunc(p, connection), size);
        }

        public static string GetSort(string sort)
        {
            return sort != string.Empty ? $@"ORDER BY {sort}" : string.Empty;
        }

        public static void SetValues(ICollection<NonEmptyString> criteria, DynamicParameters dp, DataResult like)
        {
            criteria.Add(like.Data);
            dp.AddDynamicParams(like.Parameters);
        }

        public static DataResult GetLikeCaluse(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Full);
        }

        public static DataResult GetLikeLeftCaluse(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Left);
        }

        public static DataResult GetLikeRightCaluse(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Right);
        }

        public static WhereResult GetWhereStringWithParams(ImmutableList<NonEmptyString> criteria, DynamicParameters dp)
        {
            var where = criteria.Count == 0 ? string.Empty : $" WHERE {string.Join(" AND ", criteria)} ";
            return new WhereResult(where, dp);
        }

        public static NonEmptyString GetTranslatedSort(OrderByCollection modelOrderByCollection, NonEmptyOrderByCollection defaultDatabaseOrderByCollection, ImmutableDictionary<NonEmptyString, NonEmptyString> modelDatabaseColumnMappings)
        {
            if (modelOrderByCollection.OrderBys.Count == 0)
            {
                return GetNonEmptyString(defaultDatabaseOrderByCollection.OrderBys.Select(orderBy => GetSortColumn(orderBy.Column, orderBy.Ascending)).ToImmutableList());
            }

            var dictionaryWithLowerCaseKeys = modelDatabaseColumnMappings.ToDictionary(pair => (NonEmptyLowerCaseString)pair.Key.Value, pair => pair.Value).ToImmutableDictionary();

            var result = new List<NonEmptyString>();

            foreach (var orderBy in modelOrderByCollection.OrderBys)
            {
                AddIfContainsKey(orderBy, dictionaryWithLowerCaseKeys, result);
            }

            return GetNonEmptyString(result.Count == 0 ? defaultDatabaseOrderByCollection.OrderBys.Select(orderBy => GetSortColumn(orderBy.Column, orderBy.Ascending)).ToImmutableList() : result.ToImmutableList());
        }

        private static void AddIfContainsKey(OrderBy orderBy, IDictionary<NonEmptyLowerCaseString, NonEmptyString> dictionaryWithLowerCaseKeys, ICollection<NonEmptyString> result)
        {
            var key = (NonEmptyLowerCaseString)orderBy.Column.Value;

            if (dictionaryWithLowerCaseKeys.ContainsKey(key))
            {
                result.Add(GetSortColumn(dictionaryWithLowerCaseKeys[key], orderBy.Ascending));
            }
        }

        private static NonEmptyString GetNonEmptyString(IEnumerable<NonEmptyString> list)
        {
            return (NonEmptyString)string.Join(", ", list);
        }

        private static NonEmptyString GetSortColumn(NonEmptyString column, bool ascending)
        {
            return (NonEmptyString)(column.Value.ToUpper() + " " + (ascending ? "ASC" : "DESC"));
        }

        private static DataResult GetLikeCaluseInternal(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value, LikeType likeType)
        {
            var escapeChar = (NonEmptyString)@"\";
            var dp = new DynamicParameters();
            dp.Add(paramName.Value.ToUpperInvariant(), ToLikeString(value, likeType, escapeChar).Value);
            return new DataResult((NonEmptyString)$@"{fieldName.Value.ToUpperInvariant()} LIKE @{paramName.Value.ToUpperInvariant()} ESCAPE '{escapeChar}'", dp);
        }

        private static NonEmptyString ToLikeString(string input, LikeType likeType, NonEmptyString escapeChar)
        {
            return
                likeType == LikeType.Right
                    ?
                    input.ToLikeRightString(escapeChar)
                    :
                    likeType == LikeType.Left
                        ?
                        input.ToLikeLeftString(escapeChar)
                        :
                        input.ToLikeString(escapeChar);
        }

        public class WhereResult
        {
            internal WhereResult(string where, DynamicParameters parameters)
            {
                Where = where;
                Parameters = parameters;
            }

            public string Where { get; }

            public DynamicParameters Parameters { get; }
        }

        public class DataResult
        {
            public DataResult(NonEmptyString data, DynamicParameters parameters)
            {
                Data = data;
                Parameters = parameters;
            }

            public NonEmptyString Data { get; }

            public DynamicParameters Parameters { get; }
        }
    }
}
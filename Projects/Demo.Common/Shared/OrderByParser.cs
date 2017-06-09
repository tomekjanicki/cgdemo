namespace Demo.Common.Shared
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Types;
    using Types.FunctionalExtensions;
    using ValueObjects;

    public static class OrderByParser
    {
        public static NonEmptyString GetInvalidOrderExpressionMessage(NonEmptyLowerCaseString column, ImmutableList<NonEmptyLowerCaseString> allowedColumns)
        {
            return (NonEmptyString)("Invalid order expression (it has not allowed colum). Column: " + column + ". Allowed columns: " + string.Join(", ", allowedColumns) + ".");
        }

        public static NonEmptyString GetInvalidOrderExpressionMessage(string orderBy)
        {
            return (NonEmptyString)("Invalid order expression (it has empty elements). Expression: " + orderBy + ".");
        }

        public static IResult<OrderByCollection, NonEmptyString> TryParse(string orderBy, ImmutableList<NonEmptyString> allowedColumns)
        {
            var result = new List<OrderBy>();

            var orderByTrimmed = orderBy.Trim();

            if (orderByTrimmed != string.Empty)
            {
                var allowedColumnsLowerCase = allowedColumns.Select(s => (NonEmptyLowerCaseString)s.Value).ToImmutableList();

                var nonEmptyLowerCaseStringResults = orderByTrimmed
                    .Split(',')
                    .Select(orderByItem => orderByItem.Trim())
                    .Select(orderByItemTrimmed => NonEmptyLowerCaseString.TryCreate(orderByItemTrimmed, (NonEmptyString)nameof(orderByItemTrimmed)));

                var orderByResult = GetOrderByResult(orderBy, nonEmptyLowerCaseStringResults, allowedColumnsLowerCase);

                if (orderByResult.IsFailure)
                {
                    return orderByResult.Error.GetFailResult<OrderByCollection>();
                }

                result.AddRange(orderByResult.Value);
            }

            var collectionResult = OrderByCollection.TryCreate(result.ToImmutableList());

            return collectionResult.OnSuccess(() => collectionResult.Value.GetOkMessage());
        }

        private static bool? GetSortOrder(NonEmptyLowerCaseString nonEmptyOrderByItemLowerCaseTrimmed)
        {
            return
                nonEmptyOrderByItemLowerCaseTrimmed.Value.StartsWith("+")
                ?
                true
                :
                (nonEmptyOrderByItemLowerCaseTrimmed.Value.StartsWith("-") ? (bool?)false : null);
        }

        private static IResult<ImmutableList<OrderBy>, NonEmptyString> GetOrderByResult(string orderBy, IEnumerable<IResult<NonEmptyLowerCaseString, NonEmptyString>> nonEmptyLowerCaseStringResults, ImmutableList<NonEmptyLowerCaseString> allowedColumnsLowerCase)
        {
            var result = new List<OrderBy>();

            foreach (var nonEmptyLowerCaseStringResult in nonEmptyLowerCaseStringResults)
            {
                if (nonEmptyLowerCaseStringResult.IsFailure)
                {
                    return GetInvalidOrderExpressionMessage(orderBy).GetFailResult<ImmutableList<OrderBy>>();
                }

                var addColumnResult = AddColumnOrReturnError(orderBy, nonEmptyLowerCaseStringResult.Value, allowedColumnsLowerCase, result);

                if (addColumnResult.IsFailure)
                {
                    return addColumnResult.Error.GetFailResult<ImmutableList<OrderBy>>();
                }
            }

            return result.ToImmutableList().GetOkMessage();
        }

        private static IResult<NonEmptyString> AddColumnOrReturnError(string orderBy, NonEmptyLowerCaseString nonEmptyOrderByItemLowerCaseTrimmed, ImmutableList<NonEmptyLowerCaseString> allowedColumnsLowerCase, ICollection<OrderBy> result)
        {
            var sortOrder = GetSortOrder(nonEmptyOrderByItemLowerCaseTrimmed);

            var ascending = true;

            var nonEmptyOrderByItemLowerCaseTrimmedColumn = nonEmptyOrderByItemLowerCaseTrimmed;

            if (sortOrder != null)
            {
                var column = nonEmptyOrderByItemLowerCaseTrimmed.Value.Substring(1).Trim();

                var columnNonEmptyLowerCaseStringResult = NonEmptyLowerCaseString.TryCreate(column, (NonEmptyString)nameof(column));

                if (columnNonEmptyLowerCaseStringResult.IsFailure)
                {
                    return GetInvalidOrderExpressionMessage(orderBy).GetFailResult();
                }

                ascending = sortOrder.Value;

                nonEmptyOrderByItemLowerCaseTrimmedColumn = columnNonEmptyLowerCaseStringResult.Value;
            }

            var orderByOrErrorResult = AddColumnOrReturnError(allowedColumnsLowerCase, nonEmptyOrderByItemLowerCaseTrimmedColumn, ascending, result);

            return orderByOrErrorResult.OnSuccess(Extensions.GetOkMessage);
        }

        private static IResult<NonEmptyString> AddColumnOrReturnError(ImmutableList<NonEmptyLowerCaseString> allowedColumnsLowerCase, NonEmptyLowerCaseString nonEmptyOrderByItemLowerCaseTrimmedColumn, bool ascending,  ICollection<OrderBy> result)
        {
            var orderByOrErrorResult = GetOrderByOrError(allowedColumnsLowerCase, nonEmptyOrderByItemLowerCaseTrimmedColumn, ascending);

            return orderByOrErrorResult.IsFailure ? orderByOrErrorResult.Error.GetFailResult() : Extensions.GetOkMessage().Tee(r => result.Add(orderByOrErrorResult.Value));
        }

        private static IResult<OrderBy, NonEmptyString> GetOrderByOrError(ImmutableList<NonEmptyLowerCaseString> allowedColumnsLowerCase, NonEmptyLowerCaseString nonEmptyLowerCaseStringColumn, bool ascending)
        {
            return !allowedColumnsLowerCase.Contains(nonEmptyLowerCaseStringColumn) ? GetInvalidOrderExpressionMessage(nonEmptyLowerCaseStringColumn, allowedColumnsLowerCase).GetFailResult<OrderBy>() : OrderBy.Create(nonEmptyLowerCaseStringColumn, ascending).GetOkMessage();
        }
    }
}
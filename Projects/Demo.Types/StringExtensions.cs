namespace Demo.Types
{
    using System;
    using FunctionalExtensions;
    using NullGuard;

    public static class StringExtensions
    {
        public static string IfNullReplaceWithEmptyString(this Maybe<string> input)
        {
            return input.HasNoValue ? string.Empty : input.Value;
        }

        public static string IfNullReplaceWithEmptyString([AllowNull]this string input)
        {
            return input ?? string.Empty;
        }

        public static IResult<Guid, NonEmptyString> TryParseToGuid(this string guidString, NonEmptyString fieldName)
        {
            Guid guid;
            var result = Guid.TryParse(guidString, out guid);
            return result ? guid.GetOkMessage() : ((NonEmptyString)("Unable to convert " + fieldName + " to valid guid")).GetFailResult<Guid>();
        }
    }
}

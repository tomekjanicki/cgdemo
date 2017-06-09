namespace Demo.Types
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using FunctionalExtensions;

    public static class TypeExtensions
    {
        public static IResult<Maybe<PositiveInt>, NonEmptyString> ToMaybePositiveInt(this int? value, NonEmptyString field)
        {
            return value.ToMaybe(field, PositiveInt.TryCreate);
        }

        public static IResult<Maybe<PositiveDecimal>, NonEmptyString> ToMaybePositiveDecimal(this decimal? value, NonEmptyString field)
        {
            return value.ToMaybe(field, PositiveDecimal.TryCreate);
        }

        public static IResult<Maybe<NonNegativeInt>, NonEmptyString> ToMaybeNonNegativeInt(this int? value, NonEmptyString field)
        {
            return value.ToMaybe(field, NonNegativeInt.TryCreate);
        }

        public static IResult<Maybe<NonNegativeDecimal>, NonEmptyString> ToMaybeNonNegativeDecimal(this decimal? value, NonEmptyString field)
        {
            return value.ToMaybe(field, NonNegativeDecimal.TryCreate);
        }

        public static IResult<Maybe<TResult>, NonEmptyString> ToMaybe<TResult, TIn>(this TIn? value, NonEmptyString field, Func<TIn?, NonEmptyString, IResult<TResult, NonEmptyString>> creatorFunc)
            where TResult : class
            where TIn : struct
        {
            if (value == null)
            {
                return ((Maybe<TResult>)null).GetOkMessage();
            }

            var result = creatorFunc(value, field);
            return result.IsFailure ? result.Error.GetFailResult<Maybe<TResult>>() : ((Maybe<TResult>)result.Value).GetOkMessage();
        }

        public static int? ToNullableInt(this Maybe<PositiveInt> value)
        {
            return ToNullable<int, PositiveInt>(value);
        }

        public static decimal? ToNullableDecimal(this Maybe<PositiveDecimal> value)
        {
            return ToNullable<decimal, PositiveDecimal>(value);
        }

        public static int? ToNullableInt(this Maybe<NonNegativeInt> value)
        {
            return ToNullable<int, NonNegativeInt>(value);
        }

        public static decimal? ToNullableDecimal(this Maybe<NonNegativeDecimal> value)
        {
            return ToNullable<decimal, NonNegativeDecimal>(value);
        }

        public static TRet? ToNullable<TRet, TType>(this Maybe<TType> value)
            where TRet : struct
            where TType : SimpleStructValueObject<TType, TRet>
        {
            return value.HasNoValue ? null : (TRet?)value.Value.Value;
        }

        public static IDictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            return collection.AllKeys.ToDictionary(k => k.IfNullReplaceWithEmptyString(), k => collection[k].IfNullReplaceWithEmptyString());
        }
    }
}

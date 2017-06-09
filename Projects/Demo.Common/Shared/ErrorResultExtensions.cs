namespace Demo.Common.Shared
{
    using System;
    using Types;
    using Types.FunctionalExtensions;

    public static class ErrorResultExtensions
    {
        public static IResult<Error> ToGeneric(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToGeneric<T>(this NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToNotFound<T>(this NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateNotFound(message));
        }

        public static IResult<Error> ToNotFound(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateNotFound(message));
        }

        public static IResult<Error> ToRowVersionMismatch(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateRowVersionMismatch(message));
        }

        public static IResult<T, Error> ToRowVersionMismatch<T>(this NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateRowVersionMismatch(message));
        }

        public static IResult<Error> ToForbidden(NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateForbidden(message));
        }

        public static IResult<T, Error> ToForbidden<T>(NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateForbidden(message));
        }

        public static IResult<TNextResult, Error> OnSuccess<TCurrentResult, TNextResult, TCurrentError>(this IResult<TCurrentResult, TCurrentError> result, Func<TCurrentResult, IResult<TNextResult, Error>> nextFunc, Func<TCurrentError, Error> errorConverterFunc)
            where TCurrentError : class
        {
            return Extensions.OnSuccess(result, nextFunc, errorConverterFunc);
        }

        public static IResult<TNextResult, Error> OnSuccess<TCurrentResult, TNextResult>(this IResult<TCurrentResult, Error> result, Func<TCurrentResult, IResult<TNextResult, Error>> nextFunc)
        {
            return Extensions.OnSuccess(result, nextFunc);
        }

        public static IResult<Error> OnSuccess<TResult, TError>(this IResult<TResult, TError> result, Func<TResult, IResult<Error>> nextFunc, Func<TError, Error> errorConverterFunc)
            where TError : class
        {
            return Extensions.OnSuccess(result, nextFunc, errorConverterFunc);
        }

        public static IResult<Error> OnSuccess<TResult>(this IResult<TResult, Error> result, Func<TResult, IResult<Error>> nextFunc)
        {
            return Extensions.OnSuccess(result, nextFunc);
        }
    }
}

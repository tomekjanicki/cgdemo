namespace Demo.Common.Shared
{
    using System.Collections.Immutable;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Error : ValueObject<Error>
    {
        private Error(ErrorType errorType, NonEmptyString message)
        {
            ErrorType = errorType;
            Message = message;
        }

        public ErrorType ErrorType { get; }

        public NonEmptyString Message { get; }

        public static Error CreateGeneric(NonEmptyString message)
        {
            return new Error(ErrorType.Generic, message);
        }

        public static Error CreateNotFound(NonEmptyString message)
        {
            return new Error(ErrorType.NotFound, message);
        }

        public static Error CreateRowVersionMismatch(NonEmptyString message)
        {
            return new Error(ErrorType.RowVersionMismatch, message);
        }

        public static Error CreateForbidden(NonEmptyString message)
        {
            return new Error(ErrorType.Forbidden, message);
        }

        public override string ToString()
        {
            return $"{ErrorType} : {Message}";
        }

        protected override bool EqualsCore(Error other)
        {
            return ErrorType == other.ErrorType && Message == other.Message;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { ErrorType, Message }.ToImmutableList());
        }
    }
}
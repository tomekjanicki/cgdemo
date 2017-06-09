namespace Demo.Common.ValueObjects
{
    using System.Collections.Immutable;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class IdVersion : ValueObject<IdVersion>
    {
        private IdVersion(PositiveInt id, NonEmptyString version)
        {
            Id = id;
            Version = version;
        }

        public PositiveInt Id { get; }

        public NonEmptyString Version { get; }

        public static IResult<IdVersion, NonEmptyString> TryCreate(int id, string version, NonEmptyString idField, NonEmptyString versionField)
        {
            var idResult = PositiveInt.TryCreate(id, idField);
            var versionResult = NonEmptyString.TryCreate(version, versionField);

            var result = new IResult<NonEmptyString>[]
            {
                idResult,
                versionResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new IdVersion(idResult.Value, versionResult.Value)));
        }

        protected override bool EqualsCore(IdVersion other)
        {
            return Id == other.Id && Version == other.Version;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Id, Version }.ToImmutableList());
        }
    }
}

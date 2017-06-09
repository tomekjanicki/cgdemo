namespace Demo.Logic.CQ.Customer.Update
{
    using System;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Common.ValueObjects;
    using Types;
    using Types.FunctionalExtensions;
    using ValueObjects;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion, Surname surname, Name name, PhoneNumber phoneNumber, Address address, Guid commandId)
            : base(commandId)
        {
            IdVersion = idVersion;
            Surname = surname;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public IdVersion IdVersion { get; }

        public Name Name { get; }

        public Surname Surname { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version, string name, string surname, string phoneNumber, string address)
        {
            return TryCreate(id, version, name, surname, phoneNumber, address, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version, string name, string surname, string phoneNumber, string address, Guid commandId)
        {
            var idVersionResult = IdVersion.TryCreate(id, version, (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Id), (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Version));
            var surnameResult = Surname.TryCreate(surname, (NonEmptyString)nameof(Surname));
            var nameResult = Name.TryCreate(name, (NonEmptyString)nameof(Name));
            var phoneNameResult = PhoneNumber.TryCreate(phoneNumber, (NonEmptyString)nameof(PhoneNumber));
            var addressResult = Address.TryCreate(address, (NonEmptyString)nameof(Address));

            var result = new IResult<NonEmptyString>[]
            {
                idVersionResult,
                surnameResult,
                nameResult,
                phoneNameResult,
                addressResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(idVersionResult.Value, surnameResult.Value, nameResult.Value, phoneNameResult.Value, addressResult.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && IdVersion == other.IdVersion && Surname == other.Surname && Name == other.Name && PhoneNumber == other.PhoneNumber && Address == other.Address;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { IdVersion, Surname, Name, PhoneNumber, Address, CommandId }.ToImmutableList());
        }
    }
}

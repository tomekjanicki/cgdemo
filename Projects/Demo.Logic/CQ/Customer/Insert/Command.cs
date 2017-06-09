namespace Demo.Logic.CQ.Customer.Insert
{
    using System;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Types;
    using Types.FunctionalExtensions;
    using ValueObjects;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<PositiveInt, Error>>
    {
        private Command(Name name, Surname surname, PhoneNumber phoneNumber, Address address, Guid commandId)
            : base(commandId)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public Name Name { get; }

        public Surname Surname { get; }

        public PhoneNumber PhoneNumber { get; }

        public Address Address { get; }

        public static IResult<Command, NonEmptyString> TryCreate(string name, string surname, string phoneNumber, string address)
        {
            return TryCreate(name, surname, phoneNumber, address, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(string name, string surname, string phoneNumber, string address, Guid commandId)
        {
            var nameResult = Name.TryCreate(name, (NonEmptyString)nameof(Name));
            var codeResult = Surname.TryCreate(surname, (NonEmptyString)nameof(Surname));
            var phoneNumberResult = PhoneNumber.TryCreate(phoneNumber, (NonEmptyString)nameof(PhoneNumber));
            var addressResult = Address.TryCreate(address, (NonEmptyString)nameof(Address));

            var result = new IResult<NonEmptyString>[]
            {
                codeResult,
                phoneNumberResult,
                nameResult,
                addressResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(nameResult.Value, codeResult.Value, phoneNumberResult.Value, addressResult.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && Name == other.Name && Surname == other.Surname && PhoneNumber == other.PhoneNumber && Address == other.Address;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Name, Surname, PhoneNumber, Address, CommandId }.ToImmutableList());
        }
    }
}

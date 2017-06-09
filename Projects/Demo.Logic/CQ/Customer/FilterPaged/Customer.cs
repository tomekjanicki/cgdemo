namespace Demo.Logic.CQ.Customer.FilterPaged
{
    using System;
    using Types;
    using Types.FunctionalExtensions;
    using ValueObjects;

    public class Customer
    {
        public Customer(PositiveInt id, Surname surname, Name name, PhoneNumber phoneNumber, Address address, byte[] versionInt)
            : this()
        {
            Id = id;
            Surname = surname;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            VersionInt = versionInt;
            NonEmptyString.TryCreate(Convert.ToBase64String(versionInt), (NonEmptyString)"Value").EnsureIsNotFaliure();
        }

        private Customer()
        {
        }

        public PositiveInt Id { get; }

        public Surname Surname { get; }

        public Name Name { get; }

        public PhoneNumber PhoneNumber { get; }

        public Address Address { get; }

        public NonEmptyString Version => (NonEmptyString)Convert.ToBase64String(VersionInt);

        private byte[] VersionInt { get; }
    }
}

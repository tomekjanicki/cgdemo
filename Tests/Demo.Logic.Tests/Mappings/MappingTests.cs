namespace Demo.Logic.Tests.Mappings
{
    using AutoMapper;
    using Common.Mappings;
    using Logic.CQ.Customer.Get;
    using Logic.CQ.Customer.ValueObjects;
    using Logic.Mappings;
    using NUnit.Framework;
    using Shouldly;
    using Types;
    using WebApi.Dtos.Apis.Customer.Get;

    public class MappingTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = Helper.GetMapper(AutoMapperConfiguration.Configure);
        }

        [Test]
        public void Customer_Should_Map_To_CustomerDto()
        {
            const int id = 1;
            const string surname = "surname";
            const string name = "name";
            const string phoneNumber = "phoneNumber";
            const string address = "address";
            var source = new Customer((PositiveInt)id, (Surname)surname, (Name)name, (PhoneNumber)phoneNumber, (Address)address, new byte[] { 1 });
            var result = _mapper.Map<ResponseCustomer>(source);
            result.Id.ShouldBe(id);
            result.Surname.ShouldBe(surname);
            result.Name.ShouldBe(name);
            result.PhoneNumber.ShouldBe(phoneNumber);
            result.Address.ShouldBe(address);
            result.Version.ShouldNotBeNullOrEmpty();
        }
    }
}

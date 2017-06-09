namespace Demo.WebApi.Dtos.Apis.Customer.Put
{
    using Common.Dtos;

    public sealed class RequestCustomer
    {
        public RequestCustomer(string name, string surname, string phoneNumber, string address, string version)
        {
            Name = name.IfNullReplaceWithEmptyString();
            Version = version.IfNullReplaceWithEmptyString();
            Surname = surname.IfNullReplaceWithEmptyString();
            PhoneNumber = phoneNumber.IfNullReplaceWithEmptyString();
            Address = address.IfNullReplaceWithEmptyString();
        }

        public string Name { get; }

        public string Surname { get; }

        public string PhoneNumber { get; }

        public string Address { get; }

        public string Version { get; }
    }
}

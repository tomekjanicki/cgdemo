namespace Demo.WebApi.Dtos.Apis.Customer.FilterPaged
{
    public sealed class ResponseCustomer
    {
        public ResponseCustomer(int id, string surname, string name, string phoneNumber, string address, string version)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Version = version;
        }

        public int Id { get; }

        public string Surname { get; }

        public string Name { get; }

        public string PhoneNumber { get; }

        public string Address { get; }

        public string Version { get; }
    }
}

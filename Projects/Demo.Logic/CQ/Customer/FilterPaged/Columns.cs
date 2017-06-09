namespace Demo.Logic.CQ.Customer.FilterPaged
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Types;
    using WebApi.Dtos.Apis.Customer.FilterPaged;

    public static class Columns
    {
        public static ImmutableDictionary<NonEmptyString, NonEmptyString> GetMappings()
        {
            return new Dictionary<NonEmptyString, NonEmptyString>
            {
                { (NonEmptyString)nameof(ResponseCustomer.Id), (NonEmptyString)"ID" },
                { (NonEmptyString)nameof(ResponseCustomer.Surname), (NonEmptyString)"SURNAME" },
                { (NonEmptyString)nameof(ResponseCustomer.Name), (NonEmptyString)"NAME" },
                { (NonEmptyString)nameof(ResponseCustomer.PhoneNumber), (NonEmptyString)"PHONENUMBER" },
                { (NonEmptyString)nameof(ResponseCustomer.Address), (NonEmptyString)"ADDRESS" }
            }.ToImmutableDictionary();
        }

        public static ImmutableList<NonEmptyString> GetAllowedColumns()
        {
            return GetMappings().Keys.ToImmutableList();
        }
    }
}

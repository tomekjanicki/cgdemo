namespace Demo.Logic.Mappings
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using AutoMapper;
    using Common.Dtos;
    using Common.Mappings.TypeConverters;
    using CQ.Customer.FilterPaged;
    using WebApi.Dtos.Apis.Customer.FilterPaged;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap(typeof(Common.ValueObjects.Paged<>), typeof(Paged<>)).ConvertUsing(typeof(PagedConverter<,>));
            expression.CreateMap(typeof(IEnumerable<>), typeof(ImmutableList<>)).ConvertUsing(typeof(ImmutableListConverter<,>));

            expression.CreateMap<Customer, ResponseCustomer>();
            expression.CreateMap<CQ.Customer.Get.Customer, WebApi.Dtos.Apis.Customer.Get.ResponseCustomer>();
        }
    }
}

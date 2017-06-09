namespace Demo.Common.Mappings
{
    using System;
    using AutoMapper;

    public static class Helper
    {
        public static IMapper GetMapper(Action<IMapperConfigurationExpression> configurationAction)
        {
            var configuration = new MapperConfiguration(configurationAction);
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}

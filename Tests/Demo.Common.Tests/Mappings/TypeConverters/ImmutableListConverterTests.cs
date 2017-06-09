namespace Demo.Common.Tests.Mappings.TypeConverters
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using AutoMapper;
    using Common.Mappings;
    using Common.Mappings.TypeConverters;
    using NUnit.Framework;
    using Shouldly;

    public class ImmutableListConverterTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = Helper.GetMapper(Configure);
        }

        [Test]
        public void ConverterShouldWork()
        {
            const string value = "value";

            var source = new List<string> { value };

            var result = _mapper.Map<ImmutableList<string>>(source);

            result.Count.ShouldBe(source.Count);

            result.First().ShouldBe(value);
        }

        private static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap(typeof(IEnumerable<>), typeof(ImmutableList<>)).ConvertUsing(typeof(ImmutableListConverter<,>));
        }
    }
}
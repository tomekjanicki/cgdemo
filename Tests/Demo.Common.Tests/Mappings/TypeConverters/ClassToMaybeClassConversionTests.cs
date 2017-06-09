namespace Demo.Common.Tests.Mappings.TypeConverters
{
    using AutoMapper;
    using Common.Mappings;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class ClassToMaybeClassConversionTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = Helper.GetMapper(Configure);
        }

        [Test]
        public void ConverterWithNullValueShouldWork()
        {
            var source = new From(null);

            var result = _mapper.Map<To>(source);

            result.Value.HasNoValue.ShouldBeTrue();
        }

        [Test]
        public void ConverterWithNonNullValueShouldWork()
        {
            const string value = "value";

            var source = new From(value);

            var result = _mapper.Map<To>(source);

            result.Value.HasValue.ShouldBeTrue();

            result.Value.Value.ShouldBe(value);
        }

        private static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<From, To>();
        }

        public class From
        {
            public From(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }

        public class To
        {
            public Maybe<string> Value { get; set; }
        }
    }
}
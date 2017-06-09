namespace Demo.Common.Tests.Mappings.TypeConverters
{
    using AutoMapper;
    using Common.Mappings;
    using Common.Mappings.TypeConverters;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class MaybeClassToClassConvertTests
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

            result.Value.ShouldBeNull();
        }

        [Test]
        public void ConverterWithNonNullValueShouldWork()
        {
            const string value = "value";

            var source = new From(value);

            var result = _mapper.Map<To>(source);

            result.Value.ShouldBe(value);
        }

        private static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<From, To>();
            expression.CreateMap<Maybe<string>, string>().ConvertUsing(new MaybeClassToClassConvert<string, string>());
        }

        public class From
        {
            public From(Maybe<string> value)
            {
                Value = value;
            }

            public Maybe<string> Value { get; }
        }

        public class To
        {
            public string Value { get; set; }
        }
    }
}
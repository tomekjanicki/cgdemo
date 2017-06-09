namespace Demo.Common.Tests.Mappings.TypeConverters
{
    using AutoMapper;
    using Common.Mappings;
    using Common.Mappings.TypeConverters;
    using NUnit.Framework;
    using Shouldly;
    using Types;
    using Types.FunctionalExtensions;

    public class StructToMaybeClassConvertTests
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
            var from = new From(null);

            var result = _mapper.Map<To>(from);

            result.ShouldNotBeNull();

            result.Value.HasNoValue.ShouldBeTrue();
        }

        [Test]
        public void ConverterWithNonNullValueShouldWork()
        {
            const int value = 5;

            var from = new From(value);

            var result = _mapper.Map<To>(from);

            result.ShouldNotBeNull();

            result.Value.HasValue.ShouldBeTrue();

            result.Value.Value.Value.ShouldBe(value);
        }

        private static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<From, To>();
            expression.CreateMap<int?, Maybe<PositiveInt>>().ConvertUsing(new StructToMaybeClassConvert<int, PositiveInt>());
        }

        public class From
        {
            public From(int? value)
            {
                Value = value;
            }

            public int? Value { get; }
        }

        public class To
        {
            public Maybe<PositiveInt> Value { get; set; }
        }
    }
}
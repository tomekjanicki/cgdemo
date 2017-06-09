namespace Demo.Common.Mappings.TypeConverters
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using AutoMapper;
    using NullGuard;

    public sealed class ImmutableListConverter<TSource, TDestination> : ITypeConverter<IEnumerable<TSource>, ImmutableList<TDestination>>
    {
        public ImmutableList<TDestination> Convert(IEnumerable<TSource> source, [AllowNull] ImmutableList<TDestination> destination, ResolutionContext context)
        {
            return context.Mapper.Map<IEnumerable<TDestination>>(source).ToImmutableList();
        }
    }
}
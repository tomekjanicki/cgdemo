namespace Demo.Common.Mappings.TypeConverters
{
    using System.Collections.Immutable;
    using AutoMapper;
    using Dtos;
    using NullGuard;

    public sealed class PagedConverter<TSource, TDestination> : ITypeConverter<ValueObjects.Paged<TSource>, Paged<TDestination>>
    {
        public Paged<TDestination> Convert(ValueObjects.Paged<TSource> source, [AllowNull] Paged<TDestination> destination, ResolutionContext context)
        {
            return new Paged<TDestination>(source.Count, context.Mapper.Map<ImmutableList<TDestination>>(source.Items));
        }
    }
}
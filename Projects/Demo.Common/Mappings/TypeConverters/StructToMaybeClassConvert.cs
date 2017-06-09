namespace Demo.Common.Mappings.TypeConverters
{
    using AutoMapper;
    using NullGuard;
    using Types.FunctionalExtensions;

    public sealed class StructToMaybeClassConvert<TSource, TDestination> : ITypeConverter<TSource?, Maybe<TDestination>>
        where TDestination : class
        where TSource : struct
    {
        [return: AllowNull]
        public Maybe<TDestination> Convert(TSource? source, Maybe<TDestination> destination, ResolutionContext context)
        {
            return source == null ? null : context.Mapper.Map<TDestination>(source.Value);
        }
    }
}
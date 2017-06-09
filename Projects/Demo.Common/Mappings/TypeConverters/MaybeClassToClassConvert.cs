namespace Demo.Common.Mappings.TypeConverters
{
    using AutoMapper;
    using NullGuard;
    using Types.FunctionalExtensions;

    public sealed class MaybeClassToClassConvert<TSource, TDestination> : ITypeConverter<Maybe<TSource>, TDestination>
        where TDestination : class
        where TSource : class
    {
        [return: AllowNull]
        public TDestination Convert(Maybe<TSource> source, [AllowNull] TDestination destination, ResolutionContext context)
        {
            return source.HasNoValue ? null : context.Mapper.Map<TDestination>(source.Value);
        }
    }
}
namespace Demo.Common.Mappings.TypeConverters
{
    using AutoMapper;
    using Types.FunctionalExtensions;

    public sealed class MaybeClassToStructConvert<TSource, TDestination> : ITypeConverter<Maybe<TSource>, TDestination?>
        where TDestination : struct
        where TSource : class
    {
        public TDestination? Convert(Maybe<TSource> source, TDestination? destination, ResolutionContext context)
        {
            return source.HasNoValue ? (TDestination?)null : context.Mapper.Map<TDestination>(source.Value);
        }
    }
}

namespace Demo.Common.Dtos
{
    using System.Collections.Immutable;

    public class Paged<T>
    {
        public Paged(int count, ImmutableList<T> items)
        {
            Count = count;
            Items = items;
        }

        public int Count { get; }

        public ImmutableList<T> Items { get; }
    }
}
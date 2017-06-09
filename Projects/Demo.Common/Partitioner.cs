namespace Demo.Common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Types;

    public static class Partitioner
    {
        public static ImmutableList<TResult> SplitExecuteAndGetMerged<TParam, TResult>(ImmutableList<TParam> input, Func<ImmutableList<TParam>, ImmutableList<TResult>> processingFunc, PositiveInt size)
        {
            var result = new List<TResult>();

            var subLists = input.Select((v, i) => new { Index = i, Value = v }).GroupBy(arg => arg.Index / size).Select(grouping => grouping.Select(arg => arg.Value)).ToImmutableList();

            foreach (var subList in subLists)
            {
                result.AddRange(processingFunc(subList.ToImmutableList()));
            }

            return result.ToImmutableList();
        }
    }
}

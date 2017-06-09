namespace Demo.Common.Tests
{
    using System.Collections.Immutable;
    using System.Linq;
    using NUnit.Framework;
    using Shouldly;
    using Types;

    public class PartitionerTests
    {
        [Test]
        public void SplitExecuteAndGetMerged_ShouldReturnFullList()
        {
            var size = (PositiveInt)3;
            var p = Enumerable.Range(0, size.Value * 3).ToImmutableList();
            var result = Partitioner.SplitExecuteAndGetMerged(p, ints => ints, size);
            p.Count.ShouldBe(result.Count);
        }
    }
}

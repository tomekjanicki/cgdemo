namespace Demo.Common.Tests.ValueObjects
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;
    using Test;
    using Types;

    public class OrderByCollectionTests
    {
        [Test]
        public void ValidElements_ShouldSucceed()
        {
            var r = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c1", true), OrderBy.Create((NonEmptyString)"c2", true) }.ToImmutableList());
            r.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void DuplicatedElements_ShouldFail()
        {
            var r = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true), OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());
            r.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void DuplicatedElementsWithDiffrentOrder_ShouldFail()
        {
            var r = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true), OrderBy.Create((NonEmptyString)"c", false) }.ToImmutableList());
            r.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoOrderByCollectionsWithSameValueShouldBeEqual()
        {
            var r1 = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());
            var r2 = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void TwoOrderByCollectionsWithDiffrentValueShouldNotBeEqual()
        {
            var r1 = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());
            var r2 = OrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"cx", true) }.ToImmutableList());

            Helper.ShouldNotBeEqual(r1, r2);
        }
    }
}
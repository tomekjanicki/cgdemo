namespace Demo.Common.Tests.ValueObjects
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;
    using Test;
    using Types;
    using Types.FunctionalExtensions;

    public class NonEmptyOrderByCollectionTests
    {
        [Test]
        public void ValidElements_ShouldSucceed()
        {
            var r = NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c1", true), OrderBy.Create((NonEmptyString)"c2", true) }.ToImmutableList());
            r.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void Empty_ShouldFail()
        {
            var r = NonEmptyOrderByCollection.TryCreate(new List<OrderBy>().ToImmutableList());
            r.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void ItShouldBePossibleToImplicitlyCastNonEmptyOrderByCollectionToOrderByCollection()
        {
            var value = Extensions.GetValue(() => NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c1", true) }.ToImmutableList()));
            OrderByCollection castResult = value;
            castResult.ShouldBeOfType<OrderByCollection>();
        }

        [Test]
        public void TwoNonEmptyOrderByCollectionsWithSameValueShouldBeEqual()
        {
            var r1 = NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());
            var r2 = NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void TwoNonEmptyOrderByCollectionsWithDiffrentValueShouldNotBeEqual()
        {
            var r1 = NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"c", true) }.ToImmutableList());
            var r2 = NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"cx", true) }.ToImmutableList());

            Helper.ShouldNotBeEqual(r1, r2);
        }
    }
}
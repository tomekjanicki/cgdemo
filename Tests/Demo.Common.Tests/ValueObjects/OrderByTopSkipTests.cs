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

    public class OrderByTopSkipTests
    {
        [Test]
        public void ValidValuesMinTop_ShouldSucceed()
        {
            var result = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void ValidValuesMaxTop_ShouldSucceed()
        {
            var result = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 0, Const.MaxTopSize, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InValidSkipValue_ShouldFail()
        {
            var result = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), -1, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void InValidTopValue_ShouldFail([Values(0, Const.MaxTopSize + 1)] int top)
        {
            var result = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 0, top, (NonEmptyString)"Value", (NonEmptyString)"Value");
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoOrderByTopSkipsWithSameValueShouldBeEqual()
        {
            var r1 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void TwoOrderByTopSkipsWithDifferentSkipShouldNotBeEqual()
        {
            var r1 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 0, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 1, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        [Test]
        public void TwoOrderByTopSkipsWithDifferentTopShouldNotBeEqual()
        {
            var r1 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 1, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 1, 2, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        [Test]
        public void TwoOrderByTopSkipsWithDifferentOrderByShouldNotBeEqual()
        {
            var r1 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy> { OrderBy.Create((NonEmptyString)"v1", true) }.ToImmutableList()), 1, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");
            var r2 = OrderByTopSkip.TryCreate(GetOrderByCollection(new List<OrderBy>().ToImmutableList()), 1, 1, (NonEmptyString)"Value", (NonEmptyString)"Value");

            Helper.ShouldNotBeEqual(r1, r2);
        }

        private static OrderByCollection GetOrderByCollection(ImmutableList<OrderBy> orderByList)
        {
            return Extensions.GetValue(() => OrderByCollection.TryCreate(orderByList));
        }
    }
}
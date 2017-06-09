namespace Demo.Common.Tests.ValueObjects
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;
    using Test;

    public class PagedTests
    {
        [Test]
        public void NonNegativeValue_ShouldSucceed()
        {
            var result = Paged<int>.TryCreate(0, new List<int>().ToImmutableList());
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void NegativeValue_ShouldFail()
        {
            var result = Paged<int>.TryCreate(-1, new List<int>().ToImmutableList());
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoPagedsWithSameValueShouldBeEqual()
        {
            var r1 = Paged<int>.TryCreate(0, new List<int> { 1 }.ToImmutableList());
            var r2 = Paged<int>.TryCreate(0, new List<int> { 1 }.ToImmutableList());

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void TwoPagedsWithDifferentListShouldNotBeEqual()
        {
            var r1 = Paged<int>.TryCreate(0, new List<int> { 1 }.ToImmutableList());
            var r2 = Paged<int>.TryCreate(0, new List<int> { 2 }.ToImmutableList());

            Helper.ShouldNotBeEqual(r1, r2);
        }

        [Test]
        public void TwoPagedsWithDifferentCountShouldNotBeEqual()
        {
            var r1 = Paged<int>.TryCreate(0, new List<int> { 1 }.ToImmutableList());
            var r2 = Paged<int>.TryCreate(1, new List<int> { 1 }.ToImmutableList());

            Helper.ShouldNotBeEqual(r1, r2);
        }
    }
}
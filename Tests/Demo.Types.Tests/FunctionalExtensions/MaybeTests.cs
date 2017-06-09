namespace Demo.Types.Tests.FunctionalExtensions
{
    using System;
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class MaybeTests
    {
        [Test]
        public void Can_create_a_nullable_maybe()
        {
            Maybe<MyClass> maybe = null;

            maybe.HasValue.ShouldBeFalse();
            maybe.HasNoValue.ShouldBeTrue();
        }

        [Test]
        public void Cannot_access_Value_if_none()
        {
            Maybe<MyClass> maybe = null;

            Action action = () =>
            {
                // ReSharper disable once UnusedVariable
                var myClass = maybe.Value;
            };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void Can_create_a_non_nullable_maybe()
        {
            var instance = new MyClass();

            Maybe<MyClass> maybe = instance;

            maybe.HasValue.ShouldBeTrue();
            maybe.HasNoValue.ShouldBeFalse();
            maybe.Value.ShouldBe(instance);
        }

        [Test]
        public void ToString_returns_No_Value_if_no_value()
        {
            Maybe<MyClass> maybe = null;

            var str = maybe.ToString();

            str.ShouldBe("No value");
        }

        [Test]
        public void ToString_returns_underlying_objects_string_representation()
        {
            Maybe<MyClass> maybe = new MyClass();

            var str = maybe.ToString();

            str.ShouldBe("My custom class");
        }

        [Test]
        public void Two_maybes_of_the_same_content_are_equal()
        {
            var instance = new MyClass();
            Maybe<MyClass> maybe1 = instance;
            Maybe<MyClass> maybe2 = instance;

            var equals1 = maybe1.Equals(maybe2);
            var equals2 = ((object)maybe1).Equals(maybe2);
            var equals3 = maybe1 == maybe2;
            var equals4 = maybe1 != maybe2;
            var equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.ShouldBeTrue();
            equals2.ShouldBeTrue();
            equals3.ShouldBeTrue();
            equals4.ShouldBeFalse();
            equals5.ShouldBeTrue();
        }

        [Test]
        public void Two_maybes_are_not_equal_if_differ()
        {
            Maybe<MyClass> maybe1 = new MyClass();
            Maybe<MyClass> maybe2 = new MyClass();

            var equals1 = maybe1.Equals(maybe2);
            var equals2 = ((object)maybe1).Equals(maybe2);
            var equals3 = maybe1 == maybe2;
            var equals4 = maybe1 != maybe2;
            var equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.ShouldBeFalse();
            equals2.ShouldBeFalse();
            equals3.ShouldBeFalse();
            equals4.ShouldBeTrue();
            equals5.ShouldBeFalse();
        }

        [Test]
        public void Two_empty_maybes_are_equal()
        {
            Maybe<MyClass> maybe1 = null;
            Maybe<MyClass> maybe2 = null;

            var equals1 = maybe1.Equals(maybe2);
            var equals2 = ((object)maybe1).Equals(maybe2);
            var equals3 = maybe1 == maybe2;
            var equals4 = maybe1 != maybe2;
            var equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.ShouldBeTrue();
            equals2.ShouldBeTrue();
            equals3.ShouldBeTrue();
            equals4.ShouldBeFalse();
            equals5.ShouldBeTrue();
        }

        [Test]
        public void Two_maybes_are_not_equal_if_one_of_them_empty()
        {
            Maybe<MyClass> maybe1 = new MyClass();
            Maybe<MyClass> maybe2 = null;

            var equals1 = maybe1.Equals(maybe2);
            var equals2 = ((object)maybe1).Equals(maybe2);
            var equals3 = maybe1 == maybe2;
            var equals4 = maybe1 != maybe2;
            var equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.ShouldBeFalse();
            equals2.ShouldBeFalse();
            equals3.ShouldBeFalse();
            equals4.ShouldBeTrue();
            equals5.ShouldBeFalse();
        }

        [Test]
        public void Can_compare_maybe_to_underlying_type()
        {
            var instance = new MyClass();
            Maybe<MyClass> maybe = instance;

            var equals1 = maybe.Equals(instance);

            // ReSharper disable once SuspiciousTypeConversion.Global
            var equals2 = ((object)maybe).Equals(instance);
            var equals3 = maybe == instance;
            var equals4 = maybe != instance;
            var equals5 = maybe.GetHashCode() == instance.GetHashCode();

            equals1.ShouldBeTrue();
            equals2.ShouldBeTrue();
            equals3.ShouldBeTrue();
            equals4.ShouldBeFalse();
            equals5.ShouldBeTrue();
        }

        [Test]
        public void Can_compare_underlying_type_to_maybe()
        {
            var instance = new MyClass();
            Maybe<MyClass> maybe = instance;

            var equals1 = instance == maybe;
            var equals2 = instance != maybe;

            equals1.ShouldBeTrue();
            equals2.ShouldBeFalse();
        }

        private class MyClass
        {
            public override string ToString()
            {
                return "My custom class";
            }
        }
    }
}

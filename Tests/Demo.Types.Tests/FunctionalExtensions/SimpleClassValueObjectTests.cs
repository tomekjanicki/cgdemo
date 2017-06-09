namespace Demo.Types.Tests.FunctionalExtensions
{
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class SimpleClassValueObjectTests
    {
        [Test]
        public void ToString_ShouldReturnExpectedValue()
        {
            const string v = "v1";

            var obj = new TestClass(v);

            obj.ToString().ShouldBe(v);
        }

        [Test]
        public void TwoObjectsWithTheSameValuesAreEqual()
        {
            var obj1 = new TestClass("v1");

            var obj2 = new TestClass("v1");

            obj1.ShouldBe(obj2);
        }

        [Test]
        public void TwoObjectsWithTheDiffrentValuesAreNotEqual()
        {
            var obj1 = new TestClass("v1");

            var obj2 = new TestClass("v2");

            obj1.ShouldNotBe(obj2);
        }

        public class TestClass : SimpleClassValueObject<TestClass, string>
        {
            public TestClass(string value)
                : base(value)
            {
            }
        }
    }
}

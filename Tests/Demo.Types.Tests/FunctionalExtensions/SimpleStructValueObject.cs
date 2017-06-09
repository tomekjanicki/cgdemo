namespace Demo.Types.Tests.FunctionalExtensions
{
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class SimpleStructValueObject
    {
        [Test]
        public void ToString_ShouldReturnExpectedValue()
        {
            const int v = 1;

            var obj = new TestClass(v);

            obj.ToString().ShouldBe(v.ToString());
        }

        [Test]
        public void TwoObjectsWithTheSameValuesAreEqual()
        {
            var obj1 = new TestClass(1);

            var obj2 = new TestClass(1);

            obj1.ShouldBe(obj2);
        }

        [Test]
        public void TwoObjectsWithTheDiffrentValuesAreNotEqual()
        {
            var obj1 = new TestClass(1);

            var obj2 = new TestClass(2);

            obj1.ShouldNotBe(obj2);
        }

        public class TestClass : SimpleStructValueObject<TestClass, int>
        {
            public TestClass(int value)
                : base(value)
            {
            }
        }
    }
}

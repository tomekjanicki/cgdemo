namespace Demo.Types.Tests.FunctionalExtensions
{
    using NUnit.Framework;
    using Shouldly;
    using Types.FunctionalExtensions;

    public class ExtensionsTests
    {
        [Test]
        public void IfAtLeastOneFailCombineElseReturnOk_ShouldFail()
        {
            var r1 = ((NonEmptyString)"e1").GetFailResult();
            var r2 = Extensions.GetOkMessage();

            var result = new[] { r1, r2 }.IfAtLeastOneFailCombineElseReturnOk();

            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void IfAtLeastOneFailCombineElseReturnOk_ShouldSucceed()
        {
            var r1 = Extensions.GetOkMessage();
            var r2 = Extensions.GetOkMessage();

            var result = new[] { r1, r2 }.IfAtLeastOneFailCombineElseReturnOk();

            result.IsSuccess.ShouldBeTrue();
        }
    }
}

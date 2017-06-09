namespace Demo.Logic.Tests.CQ.Customer.Delete
{
    using System;
    using Common.Test;
    using Logic.CQ.Customer.Delete;
    using NUnit.Framework;
    using Shouldly;

    public class CommandTests
    {
        [Test]
        public void ValidParameters_ShouldPass()
        {
            var commandResult = Command.TryCreate(1, "X");
            commandResult.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InvalidId_ShouldFail()
        {
            var commandResult = Command.TryCreate(-1, "X");
            commandResult.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void InvalidVersion_ShouldFail()
        {
            var commandResult = Command.TryCreate(1, string.Empty);
            commandResult.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoCommands_ShouldBeEqual()
        {
            var id = Guid.NewGuid();
            var commandResult1 = Command.TryCreate(1, "X", id);
            var commandResult2 = Command.TryCreate(1, "X", id);

            Helper.ShouldBeEqual(commandResult1, commandResult2);
        }

        [Test]
        public void TwoCommands_DiffrentVersion_ShouldNotBeEqual()
        {
            var id = Guid.NewGuid();
            var commandResult1 = Command.TryCreate(1, "X", id);
            var commandResult2 = Command.TryCreate(1, "Y", id);

            Helper.ShouldNotBeEqual(commandResult1, commandResult2);
        }

        [Test]
        public void TwoCommands_DiffrentId_ShouldNotBeEqual()
        {
            var id = Guid.NewGuid();
            var commandResult1 = Command.TryCreate(1, "X", id);
            var commandResult2 = Command.TryCreate(2, "X", id);

            Helper.ShouldNotBeEqual(commandResult1, commandResult2);
        }
    }
}

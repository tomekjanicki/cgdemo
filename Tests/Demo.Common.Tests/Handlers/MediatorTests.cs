namespace Demo.Common.Tests.Handlers
{
    using System;
    using Common.Handlers;
    using Common.Handlers.Interfaces;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class MediatorTests
    {
        [Test]
        public void NonVoidQuery_ShouldExecuteHandler()
        {
            var query = Substitute.For<IRequest<string>>();

            var handler = Substitute.For<IRequestHandler<IRequest<string>, string>>();

            handler.Handle(Arg.Any<IRequest<string>>()).Returns(string.Empty);

            var mediator = new Mediator(type => handler);

            var result = mediator.Send(query);

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void VoidQuery_ShouldExecuteHandler()
        {
            var query = Substitute.For<IRequest>();

            var handler = Substitute.For<IVoidRequestHandler<IRequest>>();

            var mediator = new Mediator(type => handler);

            mediator.Send(query);
        }

        [Test]
        public void InstanceFactoryThrowException_ShouldThrowInvalidOperationException()
        {
            var query = Substitute.For<IRequest>();

            var mediator = new Mediator(type => throw new Exception());

            Action a = () => mediator.Send(query);

            a.ShouldThrow<InvalidOperationException>();
        }
    }
}

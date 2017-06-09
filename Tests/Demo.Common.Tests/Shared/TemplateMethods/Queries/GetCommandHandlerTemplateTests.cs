namespace Demo.Common.Tests.Shared.TemplateMethods.Queries
{
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Common.Shared.TemplateMethods.Queries;
    using Common.Shared.TemplateMethods.Queries.Interfaces;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;
    using Types;
    using Types.FunctionalExtensions;

    public class GetCommandHandlerTemplateTests
    {
        private IRepository _repository;
        private GetCommandHandlerTemplate<IQuery, IRepository, string> _handler;
        private IQuery _query;

        public interface IQuery : IId, IRequest<IResult<string, Error>>
        {
        }

        public interface IRepository : IGetRepository<string>
        {
        }

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _query = Substitute.For<IQuery>();
            _handler = Substitute.For<GetCommandHandlerTemplate<IQuery, IRepository, string>>(_repository);
        }

        [Test]
        public void ShouldSucceed()
        {
            var id = (PositiveInt)1;
            const string value = "value";
            _query.Id.Returns(id);
            _repository.Get(id).Returns(value);
            var r = _handler.Handle(_query);
            r.IsSuccess.ShouldBeTrue();
            r.Value.ShouldBe(value);
        }

        [Test]
        public void NotExists_ShouldFail()
        {
            var id = (PositiveInt)1;
            const string value = null;
            _query.Id.Returns(id);
            _repository.Get(id).Returns(value);
            var r = _handler.Handle(_query);
            r.IsFailure.ShouldBeTrue();
            r.Error.ErrorType.ShouldBe(ErrorType.NotFound);
        }
    }
}

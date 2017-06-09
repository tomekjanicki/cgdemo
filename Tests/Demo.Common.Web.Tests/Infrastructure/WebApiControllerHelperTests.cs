namespace Demo.Common.Web.Tests.Infrastructure
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Results;
    using NSubstitute;
    using NUnit.Framework;
    using Shared;
    using Shouldly;
    using Types;
    using Types.FunctionalExtensions;
    using Web.Infrastructure;

    public class WebApiControllerHelperTests
    {
        [Test]
        public void GetHttpActionResult_Ok_ShouldReturnOkNegotiatedContentResult()
        {
            const string content = "Ok";
            var result = Result<string, Error>.Ok(content);
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, GetController());
            var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<string>;
            okNegotiatedContentResult.ShouldNotBeNull();
            okNegotiatedContentResult.Content.ShouldBe(content);
        }

        [Test]
        public void GetHttpActionResult_Generic_ShouldReturnBadRequestResult()
        {
            const string error = "error";
            var result = ((NonEmptyString)error).ToGeneric<string>();
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, GetController());
            var badRequestErrorMessageResult = actionResult as StatusCodeTextPlainActionResult;
            badRequestErrorMessageResult.ShouldNotBeNull();
            badRequestErrorMessageResult.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            badRequestErrorMessageResult.Message.ShouldBe(error);
        }

        [Test]
        public void GetHttpActionResult_NotFound_ShouldReturnNotFoundResult()
        {
            var result = ((NonEmptyString)"error").ToNotFound<string>();
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, GetController());
            var notFoundResult = actionResult as StatusCodeTextPlainActionResult;
            notFoundResult.ShouldNotBeNull();
            notFoundResult.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetHttpActionResult_PreconditionFailed_ShouldReturnStatusCodeResult()
        {
            var result = ((NonEmptyString)"error").ToRowVersionMismatch<string>();
            var actionResult = WebApiControllerHelper.GetHttpActionResult(result, GetController());
            var statusCodeResult = actionResult as StatusCodeTextPlainActionResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult.StatusCode.ShouldBe(HttpStatusCode.PreconditionFailed);
        }

        [Test]
        public void GetHttpActionResultForDelete_Ok_ShouldReturnStatusCodeResult()
        {
            var result = Result<Error>.Ok();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, GetController());
            var statusCodeResult = actionResult as StatusCodeResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Test]
        public void GetHttpActionResultForDelete_Generic_ShouldReturnBadRequestResult()
        {
            const string error = "error";
            var result = ((NonEmptyString)error).ToGeneric();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, GetController());
            var badRequestErrorMessageResult = actionResult as StatusCodeTextPlainActionResult;
            badRequestErrorMessageResult.ShouldNotBeNull();
            badRequestErrorMessageResult.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            badRequestErrorMessageResult.Message.ShouldBe(error);
        }

        [Test]
        public void GetHttpActionResultForDelete_NotFound_ShouldReturnNotFoundResult()
        {
            var result = ((NonEmptyString)"error").ToNotFound();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, GetController());
            var notFoundResult = actionResult as StatusCodeTextPlainActionResult;
            notFoundResult.ShouldNotBeNull();
            notFoundResult.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetHttpActionResultForDelete_RowVersionMismatch_ShouldReturnStatusCodeResult()
        {
            var result = ((NonEmptyString)"error").ToRowVersionMismatch();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForDelete(result, GetController());
            var statusCodeResult = actionResult as StatusCodeTextPlainActionResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult.StatusCode.ShouldBe(HttpStatusCode.PreconditionFailed);
        }

        [Test]
        public void GetHttpActionResultForPut_Ok_ShouldReturnOkResult()
        {
            var result = Result<Error>.Ok();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, GetController());
            var okResult = actionResult as OkResult;
            okResult.ShouldNotBeNull();
        }

        [Test]
        public void GetHttpActionResultForPut_Generic_ShouldReturnBadRequestResult()
        {
            const string error = "error";
            var result = ((NonEmptyString)error).ToGeneric();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, GetController());
            var badRequestErrorMessageResult = actionResult as StatusCodeTextPlainActionResult;
            badRequestErrorMessageResult.ShouldNotBeNull();
            badRequestErrorMessageResult.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            badRequestErrorMessageResult.Message.ShouldBe(error);
        }

        [Test]
        public void GetHttpActionResultForPut_NotFound_ShouldReturnNotFoundResult()
        {
            var result = ((NonEmptyString)"error").ToNotFound();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, GetController());
            var notFoundResult = actionResult as StatusCodeTextPlainActionResult;
            notFoundResult.ShouldNotBeNull();
            notFoundResult.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetHttpActionResultForPut_RowVersionMismatch_ShouldReturnStatusCodeResult()
        {
            var result = ((NonEmptyString)"error").ToRowVersionMismatch();
            var actionResult = WebApiControllerHelper.GetHttpActionResultForPut(result, GetController());
            var statusCodeResult = actionResult as StatusCodeTextPlainActionResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult.StatusCode.ShouldBe(HttpStatusCode.PreconditionFailed);
        }

        private static ApiController GetController()
        {
            var apiController = Substitute.For<ApiController>();
            apiController.Request = Substitute.For<HttpRequestMessage>();
            return apiController;
        }
    }
}

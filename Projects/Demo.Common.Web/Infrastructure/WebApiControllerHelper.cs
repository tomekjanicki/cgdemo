namespace Demo.Common.Web.Infrastructure
{
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Shared;
    using Types.FunctionalExtensions;

    public static class WebApiControllerHelper
    {
        public static IHttpActionResult GetHttpActionResult<T>(IResult<T, Error> result, ApiController apiController)
        {
            return result.IsSuccess ? new OkNegotiatedContentResult<T>(result.Value, apiController) : GetErrorHttpActionResult(result, apiController);
        }

        public static IHttpActionResult GetHttpActionResult(IResult<Error> result, ApiController apiController)
        {
            return result.IsSuccess ? new StatusCodeResult(HttpStatusCode.OK, apiController) : GetErrorHttpActionResult(result, apiController);
        }

        public static IHttpActionResult GetHttpActionResultForDelete(IResult<Error> result, ApiController apiController)
        {
            return result.IsSuccess ? new StatusCodeResult(HttpStatusCode.NoContent, apiController) : GetErrorHttpActionResult(result, apiController);
        }

        public static IHttpActionResult GetHttpActionResultForPut(IResult<Error> result, ApiController apiController)
        {
            return result.IsSuccess ? new OkResult(apiController) : GetErrorHttpActionResult(result, apiController);
        }

        private static IHttpActionResult GetErrorHttpActionResult(IResult<Error> result, ApiController apiController)
        {
            switch (result.Error.ErrorType)
            {
                case ErrorType.Generic:
                    return apiController.BadRequest(result.Error.Message);
                case ErrorType.RowVersionMismatch:
                    return apiController.PreconditionFailed(result.Error.Message);
                case ErrorType.NotFound:
                    return apiController.NotFound(result.Error.Message);
                case ErrorType.Forbidden:
                    return apiController.Forbidden(result.Error.Message);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
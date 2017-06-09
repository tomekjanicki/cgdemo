namespace Demo.Common.Web.Infrastructure
{
    using System.Net;
    using System.Web.Http;

    public static class ApiControllerExtensions
    {
        public static StatusCodeTextPlainActionResult BadRequest(this ApiController controller, string message)
        {
            return new StatusCodeTextPlainActionResult(message, controller.Request, HttpStatusCode.BadRequest);
        }

        public static StatusCodeTextPlainActionResult NotFound(this ApiController controller, string message)
        {
            return new StatusCodeTextPlainActionResult(message, controller.Request, HttpStatusCode.NotFound);
        }

        public static StatusCodeTextPlainActionResult Forbidden(this ApiController controller, string message)
        {
            return new StatusCodeTextPlainActionResult(message, controller.Request, HttpStatusCode.Forbidden);
        }

        public static StatusCodeTextPlainActionResult PreconditionFailed(this ApiController controller, string message)
        {
            return new StatusCodeTextPlainActionResult(message, controller.Request, HttpStatusCode.PreconditionFailed);
        }
    }
}
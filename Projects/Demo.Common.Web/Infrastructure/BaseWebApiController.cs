namespace Demo.Common.Web.Infrastructure
{
    using System.Web.Http;
    using Shared;
    using Types.FunctionalExtensions;

    public abstract class BaseWebApiController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<T>(IResult<T, Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResult(result, this);
        }

        protected IHttpActionResult GetHttpActionResult(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResult(result, this);
        }

        protected IHttpActionResult GetHttpActionResultForDelete(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResultForDelete(result, this);
        }

        protected IHttpActionResult GetHttpActionResultForPut(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResultForPut(result, this);
        }
    }
}
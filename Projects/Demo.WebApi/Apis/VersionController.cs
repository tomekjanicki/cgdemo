namespace Demo.WebApi.Apis
{
    using System.Web.Http;
    using Common.Web.Infrastructure;
    using Logic.Facades.Apis;

    public sealed class VersionController : BaseWebApiController
    {
        private readonly VersionGetFacade _versionGetFacade;

        public VersionController(VersionGetFacade versionGetFacade)
        {
            _versionGetFacade = versionGetFacade;
        }

        public IHttpActionResult Get()
        {
            var result = _versionGetFacade.Get();

            return GetHttpActionResult(result);
        }
    }
}
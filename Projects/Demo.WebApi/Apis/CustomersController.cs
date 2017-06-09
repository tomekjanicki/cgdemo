namespace Demo.WebApi.Apis
{
    using System.Web.Http;
    using Common.Web.Infrastructure;
    using Dtos.Apis.Customer.Post;
    using Logic.Facades.Apis;
    using Types;

    public sealed class CustomersController : BaseWebApiController
    {
        private readonly CustomersFilterPagedFacade _customersFilterPagedFacade;
        private readonly CustomersGetFacade _customersGetFacade;
        private readonly CustomersDeleteFacade _customersDeleteFacade;
        private readonly CustomersPutFacade _customersPutFacade;
        private readonly CustomersPostFacade _customersPostFacade;

        public CustomersController(CustomersFilterPagedFacade customersFilterPagedFacade, CustomersGetFacade customersGetFacade, CustomersDeleteFacade customersDeleteFacade, CustomersPutFacade customersPutFacade, CustomersPostFacade customersPostFacade)
        {
            _customersFilterPagedFacade = customersFilterPagedFacade;
            _customersGetFacade = customersGetFacade;
            _customersDeleteFacade = customersDeleteFacade;
            _customersPutFacade = customersPutFacade;
            _customersPostFacade = customersPostFacade;
        }

        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string orderBy = null)
        {
            var result = _customersFilterPagedFacade.FilterPaged(skip, top, orderBy.IfNullReplaceWithEmptyString());

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Put(int id, Dtos.Apis.Customer.Put.RequestCustomer requestCustomer)
        {
            var result = _customersPutFacade.Put(id, requestCustomer);

            return GetHttpActionResultForPut(result);
        }

        public IHttpActionResult Post(RequestCustomer requestCustomer)
        {
            var result = _customersPostFacade.Post(requestCustomer);

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = _customersGetFacade.Get(id);

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Delete(int id, string version)
        {
            var result = _customersDeleteFacade.Delete(id, version.IfNullReplaceWithEmptyString());

            return GetHttpActionResultForDelete(result);
        }
    }
}

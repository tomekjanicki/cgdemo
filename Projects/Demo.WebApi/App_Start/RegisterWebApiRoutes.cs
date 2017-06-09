namespace Demo.WebApi
{
    using System.Web.Http;

    public static class RegisterWebApiRoutes
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}

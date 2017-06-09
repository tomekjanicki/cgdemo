namespace Demo.WebApi
{
    using System.Web.Http;
    using Common.Log4Net;
    using Infrastructure;
    using log4net;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Owin;

    public sealed class Startup
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Startup));

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(GlobalHttpModule));
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            RegisterContainer.Execute(httpConfiguration);
            RegisterWebApiMiscs.Execute(httpConfiguration);
            RegisterWebApiRoutes.Execute(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
            Logger.Info(() => "Application started");
        }
    }
}
namespace Demo.WebApi.Infrastructure
{
    using System.Web.Http.ExceptionHandling;
    using Common.Log4Net;
    using log4net;

    public sealed class GlobalWebApiExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalWebApiExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(() => "An unhandled exception has occured", () => context.Exception);
        }
    }
}
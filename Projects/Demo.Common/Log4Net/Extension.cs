namespace Demo.Common.Log4Net
{
    using System;
    using log4net;

    public static class Extension
    {
        public static void Debug(this ILog log, Func<object> func)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(func());
            }
        }

        public static void Info(this ILog log, Func<object> func)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(func());
            }
        }

        public static void Warn(this ILog log, Func<object> func)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(func());
            }
        }

        public static void Fatal(this ILog log, Func<object> func)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(func());
            }
        }

        public static void Error(this ILog log, Func<object> func)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(func());
            }
        }

        public static void Error(this ILog log, Func<object> func, Func<Exception> funcEx)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(func(), funcEx());
            }
        }
    }
}

namespace Demo.Common.Database
{
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using Types;

    public static class DatabaseExtension
    {
        public static IDbConnection GetOpenConnection(NonEmptyString key)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[key];
            var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            var connection = factory.CreateConnection();
            Debug.Assert(connection != null, $"{nameof(connection)} != null");
            connection.ConnectionString = connectionString.ConnectionString;
            connection.Open();
            return connection;
        }

        public static NonEmptyString ToLikeString(this string input, NonEmptyString escapeChar)
        {
            return (NonEmptyString)$"%{input.ToLikeStringInternal(escapeChar)}%";
        }

        public static NonEmptyString ToLikeLeftString(this string input, NonEmptyString escapeChar)
        {
            return (NonEmptyString)$"%{input.ToLikeStringInternal(escapeChar)}";
        }

        public static NonEmptyString ToLikeRightString(this string input, NonEmptyString escapeChar)
        {
            return (NonEmptyString)$"{input.ToLikeStringInternal(escapeChar)}%";
        }

        private static string ToLikeStringInternal(this string input, NonEmptyString escapeChar)
        {
            input = input.Replace(escapeChar, string.Format("{0}{0}", escapeChar));
            input = input.Replace("%", $"{escapeChar}%");
            input = input.Replace("_", $"{escapeChar}_");
            input = input.Replace("[", $"{escapeChar}[");
            return input;
        }
    }
}

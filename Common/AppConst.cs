using System;

namespace Thex.Common
{
    public class AppConsts
    {
        public const string LocalizationSourceName = "Sensores";
        public const string DatabaseModification = "Database modification";
        public const string StartRequest = "Start request";
        public const string EndRequest = "End request";
        public const string Key = "Sensores@2020";
        public const string Exception = "Exception log";

        public static readonly string[] MethodsLog = { "PUT", "PATCH", "DELETE" };
        public static readonly string[] LatamCountries = { "BR"};

        private const string ENVIRONMENT_VARIABLE = "ASPNETCORE_ENVIRONMENT";
        private const string DEV_ENVIRONMENT_VARIABLE = "DEVELOPMENT";
        public const string DEFAULT_TIME_ZONE_NAME = "America/Rio_de_Janeiro";
        public const string DEFAULT_CULTURE_INFO_NAME = "en-US";

        public static bool IsDevelopment()
        {
            var environmentName = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE);
            if (environmentName.ToUpperInvariant() == DEV_ENVIRONMENT_VARIABLE)
                return true;

            return false;
        }
    }
}

using System;

namespace Love.Discussion.Core
{
    public class AppConstants
    {
        public static TimeSpan DEFAULT_CACHE_EXPIRACY = TimeSpan.FromMinutes(1);
        public static string AZURE_CONFIG_CACHE_SENTINEL = "Settings:CacheRefresh";
    }
}

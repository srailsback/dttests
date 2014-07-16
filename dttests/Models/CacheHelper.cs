using System;
using System.Web;
using System.Web.Caching;

namespace dttests.Models
{
    public static class CacheHelper
    {
        public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator)
        {
            var result = cache[key];
            if (result == null)
            {
                result = generator();
                cache[key] = result;
            }
            return (T)result;
        }

        public static void ClearCache(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                if (HttpRuntime.Cache.Get(key) != null)
                {
                    HttpRuntime.Cache.Remove(key);
                }
            }
        }
    }
}
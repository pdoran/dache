using System;

namespace Dache.CacheHost.Storage
{
    /// <summary>
    /// Contains an instance of a mem cache.
    /// </summary>
    public static class MemCacheContainer
    {
        // The instance
        private static IMemCache _instance = null;

        /// <summary>
        /// The mem cache instance.
        /// </summary>
        public static IMemCache Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                // Sanitize
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (_instance != null)
                {
                    throw new NotSupportedException("The mem cache instance cannot be set more than once");
                }

                _instance = value;
            }
        }
    }
}

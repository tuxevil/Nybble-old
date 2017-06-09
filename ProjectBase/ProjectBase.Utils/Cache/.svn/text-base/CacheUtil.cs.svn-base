using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace ProjectBase.Utils.Cache
{
    /// <summary>
    /// General purpose class for cache utilities
    /// </summary>
    public class CacheManager
    {
        #region Internal Cache 
        private static System.Web.Caching.Cache _cache
        {
            get {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Cache;
                else
                    return null;
            }
        }
        #endregion

        #region Get Methods

        /// <summary>
        /// Returns an item in the cache.
        /// </summary>
        /// <param name="key">Item key</param>
        /// <returns>An item or null if not found</returns>
        public static object GetCached(string key)
        {
            return (_cache != null) ? _cache.Get(key) : null;
        }

        /// <summary>
        /// Returns an item in the cache.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key">Item key</param>
        /// 
        /// <returns>An item or null if not found</returns>
        public static object GetCached(Type type, string key)
        {
            return GetCached(type.ToString() + key);
        }

        #endregion

        #region Add Methods

        /// <summary>
        /// Adds an item to the cache for two hours.
        /// </summary>
        /// <param name="key">Item key</param>
        /// <param name="value">Item value</param>
        /// <returns>The item beign added or null if not possible.</returns>
        public static object AddItem(string key, object value)
        {
            return AddItem(key, value, new TimeSpan(2, 0, 0));
        }

        /// <summary>
        /// Add an item to the cache for two hours
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object AddItem(Type type, string key, object value)
        {
            return AddItem(type.ToString() + key, value, new TimeSpan(2, 0, 0));
        }

        /// <summary>
        /// Adds an item to the cache for the required time.
        /// </summary>
        /// <param name="key">Item key</param>
        /// <param name="value">Item value</param>
        /// <param name="ts">TimeSpan to add item to the cache</param>
        /// <returns>The item beign added or null if not possible.</returns>
        public static object AddItem(string key, object value, TimeSpan ts)
        {
            if (_cache != null)
                return _cache.Add(key, value, null, DateTime.MaxValue, ts, CacheItemPriority.Normal, null);
            else
                return null;
        }

        #endregion

        #region Expiration Methods

        /// <summary>
        /// Expires an item in the cache
        /// </summary>
        /// <param name="key">Item key</param>
        /// <returns>The item beign expired</returns>
        public static object ExpireItem(string key)
        {
            if (_cache != null)
                return _cache.Remove(key);
            else
                return null;
        }

        /// <summary>
        /// Removes an item in the cache
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object ExpireItem(Type type, string key)
        {
            return ExpireItem(type + key);
        }

        public static void ExpireAll(Type type)
        {
            ExpireAll(type.ToString());
        }

        public static void ExpireAll(string startsWith)
        {
            if (_cache == null)
                return;

            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();

            List<string> keyList = new List<string>();
            while (CacheEnum.MoveNext())
                if (string.IsNullOrEmpty(startsWith) || CacheEnum.Key.ToString().StartsWith(startsWith))
                    keyList.Add(CacheEnum.Key.ToString());

            foreach (string key in keyList)
                _cache.Remove(key);
        }

        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void ExpireAll()
        {
            ExpireAll(string.Empty);
        }

        #endregion
    }
}

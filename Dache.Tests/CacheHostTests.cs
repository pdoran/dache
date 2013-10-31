using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using Dache.CacheHost.Storage;
using System.Runtime.Caching;
using System.Diagnostics;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Dache.CacheHost;
using Dache.CacheHost.Configuration;
using Dache.Core.Performance;
using Dache.Client;


namespace Dache.Tests
{
    [TestClass()]
    public class CacheHostTests
    {

        private const string TestName = "DacheTest";

        private NameValueCollection _Config;
        private IMemCache _memCache;

        private NameValueCollection Config
        {
            get
            {
                if (_Config == null)
                {
                    var config = new NameValueCollection();
                    config.Add("pollingInterval", "00:00:15");
                    config.Add("physicalMemoryLimitPercentage", "20");
                    _Config = config;
                }
                
                return _Config;
            }
        }

        private IMemCache GetCache()
        {
            return _memCache;
        }

        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new Castle.Windsor.WindsorContainer();
            container.Install(new MemcacheInstaller("Dache_Test",false));

            _memCache = container.Resolve<IMemCache>(new { cacheName = TestName, cacheConfig = Config });
        }

        [TestMethod]
        public void EnsureCanAdd() {
            var key = Guid.NewGuid().ToString();
            var value = new byte[100];
            var itemPolicy = new CacheItemPolicy();
            var memcache = GetCache();
            memcache.Add(key, value, itemPolicy);
            Assert.AreEqual(value, memcache.Get(key));
        }

        [TestMethod]
        public void CanAdd1000Items()
        {
            var memcache = GetCache();
            string key = "";
            var itemCount = 1000;
            for (var i = 0; i < itemCount; i++)
            {
                key = Add(memcache);
            }
            Assert.AreEqual(itemCount, memcache.GetCount());
            Assert.IsNotNull(memcache.Get(key));
        }

        [TestMethod]
        public void CanUpdateItem()
        {
            var memcache = GetCache();
            var key = Guid.NewGuid().ToString();
            var value = new byte[100];
            memcache.Add(key, value, new CacheItemPolicy());
            Assert.AreEqual(value, memcache.Get(key));
            var newValue = new byte[1000];
            memcache.Add(key, newValue, new CacheItemPolicy());
            Assert.AreNotEqual(value, memcache.Get(key));
            Assert.AreEqual(newValue, memcache.Get(key));
        }

        [TestMethod]
        public void CanRemoveItem()
        {
            var memcache = GetCache();
            var key = Guid.NewGuid().ToString();
            var value = new byte[100];
            memcache.Add(key, value , new CacheItemPolicy());
            Assert.AreEqual(value, memcache.Get(key));
            memcache.Remove(key);
            Assert.IsNull(memcache.Get(key));
        }

        [TestMethod]
        public void EnsureGetOnNonExistantKeyReturnsNull()
        {
            var memcache = GetCache();
            Assert.IsNull(memcache.Get(Guid.NewGuid().ToString()));
        }

        private string Add(IMemCache cache)
        {
            var itemPolicy = new CacheItemPolicy();
            var key = Guid.NewGuid().ToString();
            cache.Add(key, new byte[100], itemPolicy);
            return key;
        }
    }
}

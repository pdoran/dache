using Dache.Client.Configuration;
using Dache.Client.Serialization;
using Dache.Core.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Dache.Client.Exceptions;
using Dache.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dache.Tests
{
    [TestClass()]
    class CacheClientTests
    {

        [TestMethod]
        public void TestClient()
        {
            var key = Guid.NewGuid().ToString();
            var obj = new object();
            var client = new CacheClient();
            client.AddOrUpdate(key, obj); 
            object actual = null;
            client.TryGet(key, out actual);
            Assert.AreEqual(obj, actual);
        }
        
        
    }
}

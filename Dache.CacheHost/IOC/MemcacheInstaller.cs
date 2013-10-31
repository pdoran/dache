using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Dache.CacheHost.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dache.CacheHost
{
    public class MemcacheInstaller : IWindsorInstaller
    {
        private readonly string _performanceCounterInstanceName;
        private readonly bool _readOnly;
        public MemcacheInstaller(string performanceCounterInstanceName, bool readOnly)
        {
            _performanceCounterInstanceName = performanceCounterInstanceName;
            _readOnly = readOnly;
        }

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<Dache.Core.Performance.CustomPerformanceCounterManager>().UsingFactoryMethod((c,p) =>
            {
                return new Dache.Core.Performance.CustomPerformanceCounterManager(_performanceCounterInstanceName, _readOnly);
            }).LifestyleSingleton());
            container.Register(Component.For<PerformanceCounterInterceptor>());
            container.Register(
                Component.For<IMemCache>()
                .ImplementedBy<MemCache>()
                .Interceptors<PerformanceCounterInterceptor>()
            );
        }
    }
}

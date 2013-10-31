using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core;
using Castle.DynamicProxy;
using Dache.CacheHost.Storage;
using Dache.CacheHost.AOP;
using Dache.Core.Performance;

namespace Dache.CacheHost
{
    public class PerformanceCounterInterceptor : IInterceptor
    {
        private CustomPerformanceCounterManager _PerformanceManager;

        public PerformanceCounterInterceptor(CustomPerformanceCounterManager performanceCounterManager)
        {
            _PerformanceManager = performanceCounterManager;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (_PerformanceManager != null)
            {

                var attrs = Attribute.GetCustomAttributes(invocation.MethodInvocationTarget).Where(a => a.GetType().BaseType == typeof(CounterAttribute));
                foreach (var attr in attrs)
                {
                    ((CounterAttribute)attr).ApplyPerformanceCounter(_PerformanceManager);
                }
            }
            
        }
    }
}

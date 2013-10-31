using Dache.Core.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dache.CacheHost.Storage
{
    public abstract class CounterAttribute : Attribute
    {
        public abstract void ApplyPerformanceCounter(CustomPerformanceCounterManager manager);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class Track
    {
        public string Name { get; set; }

        public IEnumerable<Slot> Sessions { get; set; }

        public TimeSpan RemainingUnallocatedTime()
        {
            return new TimeSpan(0, Convert.ToInt32(420 - Sessions.Sum(x => x.Duration.TotalMinutes)), 0);
        }
    }
}
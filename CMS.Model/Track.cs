using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class Track
    {
        public string Name { get; set; }

        public IEnumerable<Slot> Sessions { get; set; }
    }
}
using System;

namespace CMS
{
    public abstract class Slot
    {
        public virtual string Title { get; set; }

        public virtual TimeSpan StartTime { get; set; }

        public virtual TimeSpan Duration { get; set; }
    }
}
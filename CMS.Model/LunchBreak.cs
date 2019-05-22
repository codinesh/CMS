using System;
using System.Diagnostics.Contracts;

namespace CMS
{
    public class LunchBreak : Slot
    {
        public LunchBreak(TimeSpan duration, TimeSpan startTime, string title = "Lunch Break")
        {
            Contract.Requires(title != null);

            this.Title = title;
            this.Duration = duration;
            this.StartTime = startTime;
        }

        public override string Title { get => base.Title; }
        
        public override TimeSpan Duration { get => base.Duration; }

        public override TimeSpan StartTime { get => base.StartTime; }
    }
}
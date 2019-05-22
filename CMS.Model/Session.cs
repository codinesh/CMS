using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class Session : Slot
    {
        public IList<Slot> Talks { get; }

        public SessionType SessionType { get; set; }

        public Session()
        {
            this.Talks = new List<Slot>();
        }

        public bool AddTalk(Talk talk)
        {
            var canAddTalk = RemainingUnallocatedTime() >= talk.Duration;
            if (canAddTalk)
            {
                Talks.Add(talk);
            }

            return canAddTalk;
        }

        private TimeSpan RemainingUnallocatedTime()
        {
            return new TimeSpan(0, Convert.ToInt32(420 - Talks.Sum(x => x.Duration.TotalMinutes)), 0);
        }
    }
}
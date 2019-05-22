using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class AppSettings
    {
        public string Title { get; set; }
    }

    public class Talk : Slot
    {
        public Talk(string title, int duration)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Duration = new TimeSpan(0, duration, 0);
        }
    }

    public abstract class Slot
    {
        public string Title { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan Duration { get; set; }
    }

    public class Session : Slot
    {
        public List<Slot> Talks { get; set; }

        public SessionType SessionType { get; set; }
    }

    public class LunchBreak : Slot
    {
        public LunchBreak()
        {
            this.Duration = new TimeSpan(0, 60, 0);
        }
    }

    public class NetworkingEvent : Slot
    {
    }

    public class Track
    {
        public string Name { get; set; }

        public List<Slot> Sessions { get; set; }

        public TimeSpan RemainingUnallocatedTime()
        {
            return new TimeSpan(0, Convert.ToInt32(420 - Sessions.Sum(x => x.Duration.TotalMinutes)), 0);
        }
    }

    public class Conference
    {
        public List<Track> Tracks { get; set; }

        public DateTime ConferenceStartDate { get; set; }
    }

    public enum SessionType
    {
        MorningSession = 0,
        LunchBreak = 1,
        AfternoonSession = 2,
        NetworkingEvent = 3
    }
}
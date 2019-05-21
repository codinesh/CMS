using System;
using System.Collections.Generic;
using System.Text;

namespace CMS
{
    public class Talk
    {
        public Talk(string title, int duration)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Duration = new TimeSpan(0, duration, 0);
        }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }
    }

    public abstract class Slot
    {
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }
    }

    public class Session : Slot
    {

        public List<Talk> Talks { get; set; }

        public SessionType SessionType { get; set; }
    }

    public class LunchBreak : Slot
    {
    }

    public class NetworkingEvent : Slot
    {
    }

    public class Track
    {
        public string Name { get; set; }

        public List<Slot> Sessions { get; set; }
    }

    public enum SessionType
    {
        MorningSession = 0,
        AfternoonSession = 2,
    }
}


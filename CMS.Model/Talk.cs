using System;

namespace CMS
{
    public class Talk : Slot
    {
        public Talk(string title, int duration)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Duration = new TimeSpan(0, duration, 0);
        }
    }
}
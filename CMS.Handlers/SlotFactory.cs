using CMS.Model;
using System;

namespace CMS.Core
{
    public static class SlotFactory
    {
        public static Slot GetSlot(string category, string title, TimeSpan startTime, TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException(String.Empty, nameof(category));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(String.Empty, nameof(title));
            }

            Slot createdSlot = null;
            switch (category)
            {
                case "Session":
                    createdSlot = new Session { Duration = duration, StartTime = startTime, Title = title };
                    break;
                case "Lunch Break":
                    createdSlot = new LunchBreak(new TimeSpan(0, 60, 0), startTime, title);                    
                    break;
                case "Networking Session":
                    createdSlot = new NetworkingEvent(title);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return createdSlot;
        }
    }
}

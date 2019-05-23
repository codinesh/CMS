using System;

namespace CMS.Model
{
    public class SlotSetting
    {
        /// <summary>
        /// Gets or sets Category of a slot.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets Title of a Slot.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets StartTime of a Slot.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets Duration of a Slot.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets or sets Default Networking Event Start Time to use.
        /// </summary>
        public TimeSpan DefaultNetworkingEventStartTime { get; set; }
        
        public TimeSpan Duration => EndTime - StartTime;

        /// <summary>
        /// Gets a boolean value representing whether a slot can Have Talks.
        /// </summary>
        public bool CanHaveTalks { get; }
    }
}
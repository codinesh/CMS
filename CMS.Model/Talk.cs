using System;

namespace CMS.Model
{
    public class Talk : Slot
    {
        /// <summary>
        /// Initializes a new instance of Talk with title and duration.
        /// </summary>
        /// <param name="title">Title of talk.</param>
        /// <param name="duration">Duration of talk in TimeSpan.</param>
        public Talk(string title, int duration)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Duration = new TimeSpan(0, duration, 0);
        }
    }
}
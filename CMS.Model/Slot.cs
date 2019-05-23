using System;

namespace CMS.Model
{
    /// <summary>
    /// Abstract class to define structure of a Slot.
    /// </summary>
    public abstract class Slot
    {
        /// <summary>
        /// Gets or sets Title of a Slot.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets StartTime of a Slot.
        /// </summary>
        public virtual TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets Duration of a Slot.
        /// </summary>
        public virtual TimeSpan Duration { get; set; }

        /// <summary>
        /// Converts the value of current object in a string format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.StartTime} {this.Title}";
        }
    }
}
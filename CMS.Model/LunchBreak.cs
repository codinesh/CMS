using System;
using System.Diagnostics.Contracts;

namespace CMS.Model
{
    /// <summary>
    /// Implementation of Slot, to represent Lunch Break Slot.
    /// </summary>
    public class LunchBreak : Slot
    {
        /// <summary>
        /// Initializes new instalce of LunchBreak class.
        /// </summary>
        /// <param name="duration">duration in TimeSpan.</param>
        /// <param name="startTime">start time in TimeSpan.</param>
        /// <param name="title">optional parameter to represent title of Slot.</param>
        public LunchBreak(TimeSpan duration, TimeSpan startTime, string title = "Lunch Break")
        {
            Contract.Requires(title != null);

            this.Title = title;
            this.Duration = duration;
            this.StartTime = startTime;
        }

        /// <summary>
        /// Gets title of Slot.
        /// </summary>
        public override string Title { get => base.Title; }

        /// <summary>
        /// Gets Duration of Slot.
        /// </summary>
        public override TimeSpan Duration { get => base.Duration; }

        /// <summary>
        /// Gets StartTime of Slot.
        /// </summary>
        public override TimeSpan StartTime { get => base.StartTime; }
    }
}
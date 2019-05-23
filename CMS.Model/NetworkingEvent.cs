using System;
using System.Diagnostics.Contracts;

namespace CMS.Model
{
    /// <summary>
    /// Implementation of Slot to represent Networking Event.
    /// </summary>
    public class NetworkingEvent : Slot
    {
        /// <summary>
        /// Initializes new instalce of NetworkingEvent class.
        /// </summary>
        /// <param name="title">title </param>
        /// <param name="defaultStartTime">default start time value</param>
        public NetworkingEvent(TimeSpan defaultStartTime, string title = "Networking Event")
        {
            Contract.Requires(title != null);
            this.Title = title;
            this.DefaultStartTime = defaultStartTime;
        }

        /// <summary>
        /// Gets Title of NewtorkingEvent.
        /// </summary>
        public override string Title { get => base.Title; }

        /// <summary>
        /// Gets or sets the default start time.
        /// </summary>
        public TimeSpan DefaultStartTime { get; internal set; }
    }
}
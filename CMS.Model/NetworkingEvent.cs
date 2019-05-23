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
        /// <param name="title"></param>
        public NetworkingEvent(string title = "Networking Event")
        {
            Contract.Requires(title != null);
            this.Title = title;
        }

        /// <summary>
        /// Gets Title of NewtorkingEvent.
        /// </summary>
        public override string Title { get => base.Title; }
    }
}
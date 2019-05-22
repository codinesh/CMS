using System.Diagnostics.Contracts;

namespace CMS
{
    public class NetworkingEvent : Slot
    {
        public NetworkingEvent(string title = "Networking Event")
        {
            Contract.Requires(title != null);
            this.Title = title;
        }

        public override string Title { get => base.Title; }
    }
}
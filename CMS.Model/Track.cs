using System.Collections.Generic;
using System.Linq;

namespace CMS.Model
{
    /// <summary>
    /// Represents a Track in a conference.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Gets or Sets Name of the Track.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets sessions in a track.
        /// </summary>
        public List<Slot> Sessions { get; private set; } = new List<Slot>();

        /// <summary>
        /// Initializes a new instance of Track with given slots.
        /// </summary>
        /// <param name="slots">Slots to be added to the track.</param>
        public Track(string name, IEnumerable<Slot> slots)
        {
            this.Name = name;
            this.Sessions.AddRange(slots);
        }

        public void SetNetworkingEventStartTime()
        {
            var afternoonSession = this.Sessions.OfType<Session>()?.LastOrDefault();
            var lastTalk = afternoonSession.Talks?.LastOrDefault();
            var networkingSession = this.Sessions.OfType<NetworkingEvent>()?.FirstOrDefault();
            networkingSession.StartTime = lastTalk == null ? networkingSession.DefaultStartTime : lastTalk.StartTime.Add(lastTalk.Duration);
        }
    }
}
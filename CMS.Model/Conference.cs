using System;
using System.Collections.Generic;

namespace CMS.Model
{
    /// <summary>
    /// Represents a Conference with schedule.
    /// </summary>
    public class ConferenceDetail
    {
        /// <summary>
        /// Represents the available tracks.
        /// </summary>
        public IEnumerable<Track> Tracks { get; set; }

        /// <summary>
        /// Represents the date of conference.
        /// </summary>
        public DateTime ConferenceStartDate { get; set; }
    }
}
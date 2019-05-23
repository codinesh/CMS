using System;
using System.Collections.Generic;

namespace CMS.Model
{
    /// <summary>
    /// Represents Conference settings
    /// </summary>
    public class ConferenceSettings
    {
        /// <summary>
        /// Represents different sessions in a conference.
        /// </summary>
        public IList<SlotSetting> Sessions {get;set;}

        /// <summary>
        /// 
        /// </summary>
        public double MaxDurationPerTrack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TrackNameFormat { get; set; }
    }
}
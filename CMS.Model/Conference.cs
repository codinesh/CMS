using System;
using System.Collections.Generic;

namespace CMS
{
    public class Conference
    {
        public IEnumerable<Track> Tracks { get; set; }

        public DateTime ConferenceStartDate { get; set; }
    }
}
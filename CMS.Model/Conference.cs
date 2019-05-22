using System;
using System.Collections.Generic;

namespace CMS
{
    public class ConferenceDetail
    {
        public IEnumerable<Track> Tracks { get; set; }

        public DateTime ConferenceStartDate { get; set; }
    }
}
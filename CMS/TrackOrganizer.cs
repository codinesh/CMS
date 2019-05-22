using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class TrackOrganizer : ITrackOrganizer
    {
        private readonly ILogger logger;
        private readonly IOptions<AppSettings> config;
        private readonly ITalkOrganizer talkOrganizer;

        public TrackOrganizer(ILogger logger, IOptions<AppSettings> config, ITalkOrganizer talkOrganizer)
        {
            this.logger = logger;
            this.config = config;
            this.talkOrganizer = talkOrganizer;
        }

        public void Organize(int v, List<Talk> talks)
        {
            
        }

        public void Organize(List<Track> tracks, List<Talk> talks)
        {
            foreach (var talk in talks)
            {
                var track = GetNextAvailableTrack(tracks, talk);
                track.Sessions.Add()
            }
        }

        private Track GetNextAvailableTrack(List<Track> tracks, Talk talk)
        {
            return tracks.FirstOrDefault(x => x.RemainingUnallocatedTime() >= talk.Duration);
        }
    }
}

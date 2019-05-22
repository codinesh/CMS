using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS
{
    public class SchedulerService
    {
        private readonly ILogger logger;
        private readonly ITrackOrganizer trackOrganizer;
        private readonly AppSettings config;

        public SchedulerService(ILogger<SchedulerService> logger, IOptions<AppSettings> config, ITrackOrganizer trackOrganizer)
        {
            this.logger = logger;
            this.trackOrganizer = trackOrganizer;
            this.config = config.Value;
        }

        public ConferenceDetail Conference { get; private set; }

        /// <summary>
        /// Identifies the number of and initializes the tracks and schedules
        /// </summary>
        /// <param name="talks">
        /// list of talks containing name and duration.
        /// </param>
        public void Initialize(IList<Talk> talks)
        {
            var totalTalkDuration = talks.Sum(x => x.Duration.TotalMinutes);
            var maxDurationPerTrack = 420;
            var numberOfTracksRequired = Math.Ceiling(totalTalkDuration / maxDurationPerTrack);
            var tracks = InitializeTracks(numberOfTracksRequired);
            trackOrganizer.Organize(tracks, talks);
            Conference = new ConferenceDetail() { Tracks = tracks, ConferenceStartDate = DateTime.Now.Date };
        }

        public ConferenceDetail GetSchedule()
        {
            return Conference;
        }

        private IList<Track> InitializeTracks(double numberOfTracks)
        {
            logger.LogInformation("test");
            var tracks = new List<Track>();
            for (int i = 0; i < numberOfTracks; i++)
            {
                tracks.Add(new Track
                {
                    Sessions = new List<Slot>
                {
                    new Session{ Duration = new TimeSpan(0, 180, 0), StartTime = new TimeSpan(9, 0,0),  SessionType = SessionType.MorningSession, Title = "Morning Session"   },
                    new LunchBreak(new TimeSpan(0, 60, 0), new TimeSpan(12, 0 ,0)),
                    new Session{ Duration = new TimeSpan(0, 240, 0), StartTime = new TimeSpan(13,0,0), SessionType = SessionType.AfternoonSession, Title = "Afternoon Session"   },
                    new NetworkingEvent()
                }
                });
            }

            return tracks;
        }
    }
}
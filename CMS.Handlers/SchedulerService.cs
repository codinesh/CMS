using CMS.Core;
using CMS.Model;
using CMS.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Core
{
    public class SchedulerService : IScheduler
    {
        private readonly ILogger logger;
        private readonly ITrackOrganizer trackOrganizer;
        private readonly ConferenceSettings config;

        /// <summary>
        /// Initializes new instance of SchedulerService with logger, configuration detail and 
        /// the organizer defining the method to use while arranging talks in track.
        /// </summary>
        /// <param name="logger">Logger to be used.</param>
        /// <param name="config">configuration data</param>
        /// <param name="trackOrganizer">implemenation of ITrackOrganizer to be used</param>
        public SchedulerService(ILogger<SchedulerService> logger, IOptions<ConferenceSettings> config, ITrackOrganizer trackOrganizer)
        {
            this.logger = logger;
            this.trackOrganizer = trackOrganizer;
            this.config = config.Value;
        }

        public ConferenceDetail Conference { get; private set; }

        #region public methods

        /// <summary>
        /// Identifies the number of and initializes the tracks and schedules
        /// </summary>
        /// <param name="talks">
        /// list of talks containing name and duration.
        /// </param>
        /// <returns>return ConferenceDetail class with tracks scheduled with Talks.</returns>
        public ConferenceDetail Initialize(IList<Talk> talks)
        {
            logger.LogInformation($"Start: {nameof(SchedulerService)} => {nameof(Initialize)} with {nameof(talks)}:{talks.Count}");

            var tracks = InitializeTracks(GetNumberOfTracksRequired(talks));
            trackOrganizer.Organize(tracks, talks);
            Conference = new ConferenceDetail() { Tracks = tracks, ConferenceStartDate = DateTime.Now };

            logger.LogInformation($"Complete: {nameof(SchedulerService)} => {nameof(Initialize)} with {talks.Count} Talks.");
            return Conference;
        }

        #endregion

        #region private methods

        private double GetNumberOfTracksRequired(IEnumerable<Talk> talks)
        {
            var totalTalkDuration = talks.Sum(x => x.Duration.TotalMinutes);
            return Math.Ceiling(totalTalkDuration / config.MaxDurationPerTrack);
        }

        private IList<Track> InitializeTracks(double numberOfTracks)
        {
            logger.LogInformation($"Start: {nameof(SchedulerService)} => {nameof(InitializeTracks)} with {nameof(numberOfTracks)}:{numberOfTracks} .");

            var tracks = new List<Track>();
            var trackNameFormat = config.TrackNameFormat;
            for (int i = 1; i <= numberOfTracks; i++)
            {
                tracks.Add(CreateTrack(string.Format(trackNameFormat, i)));
            }

            logger.LogInformation($"Complete: {nameof(SchedulerService)} => {nameof(InitializeTracks)} with {numberOfTracks} Talks.");
            return tracks;
        }

        private Track CreateTrack(string trackName)
        {
            var slots = new List<Slot>();
            foreach (var item in config.Sessions)
            {
                slots.Add(SlotFactory.GetSlot(item.Category, item.Title, item.StartTime, item.Duration, item.DefaultNetworkingEventStartTime));
            }

            return new Track(trackName, slots);
        }
        #endregion
    }
}
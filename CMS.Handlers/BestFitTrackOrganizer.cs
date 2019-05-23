using CMS.Model;
using CMS.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Core
{
    /// <summary>
    /// Best Fit implemenattion of ITrackOrganizer to fill Talks in the Track.
    /// This is one of the variation of Bin Packing algorithm.
    /// </summary>
    public class BestFitTrackOrganizer : ITrackOrganizer
    {
        #region fields and constructors
        private readonly ILogger logger;

        /// <summary>
        /// Initializes new instance of BestFitTrackOrganizer.
        /// </summary>
        /// <param name="logger">Logger to be used.</param>
        /// <param name="config">configuration data to be used while scheduling the talks.</param>
        public BestFitTrackOrganizer(ILogger<BestFitTrackOrganizer> logger)
        {
            this.logger = logger;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Organizes the given talks into the tracks.
        /// </summary>
        /// <param name="tracks">Available tracks</param>
        /// <param name="talks">talks to arrange in tracks.</param>
        /// <returns>List of tracks with talks.</returns>          
        /// <exception>throws CanNotAddTalkException if session with enough time is not found.</exception>
        public IEnumerable<Track> Organize(IList<Track> tracks, IList<Talk> talks)
        {
            if (tracks == null)
            {
                throw new ArgumentNullException(nameof(tracks));
            }

            if (talks == null)
            {
                throw new ArgumentNullException(nameof(talks));
            }

            logger.LogInformation($"Start: {nameof(BestFitTrackOrganizer)} => {nameof(Organize)} with {tracks.Count}:{nameof(tracks)} and {talks.Count}: {nameof(talks)}");
            var bins = tracks.SelectMany(x => x.Sessions.OfType<Session>());
            foreach (var talk in talks.OrderByDescending(x => x.Duration))
            {
                var session = GetBestFit(bins, talk.Duration);
                if (session == null)
                {
                    throw new CanNotAddTalkException("No empty bin found.");
                }
                else { 
                    SetTalkStartTimeBasedOnPreviousTalk(talk, session);
                    session.AddTalk(talk);
                }
            }

            foreach (var track in tracks)
            {
                track.SetNetworkingEventStartTime();
            }

            logger.LogInformation($"Complete: {nameof(BestFitTrackOrganizer)} => {nameof(Organize)}");
            return tracks;
        }

        #endregion

        #region private methods
        private static void SetTalkStartTimeBasedOnPreviousTalk(Talk talk, Session bin)
        {
            if (bin.Talks.Any())
            {
                var previousTalk = bin.Talks.Last();
                talk.StartTime = previousTalk.StartTime.Add(previousTalk.Duration);
            }
            else
            {
                talk.StartTime = bin.StartTime;
            }
        }

        private Session GetBestFit(IEnumerable<Session> bins, TimeSpan duration)
        {
            return bins.FirstOrDefault(b => b.Duration.TotalMinutes - b.Talks.Sum(x => x.Duration.TotalMinutes) >=  duration.TotalMinutes);
        }
        #endregion
    }
}

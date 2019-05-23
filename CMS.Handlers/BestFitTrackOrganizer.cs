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

        /// <summary>
        /// Organizes the given talks into the tracks.
        /// </summary>
        /// <param name="tracks">Available tracks</param>
        /// <param name="talks">talks to arrange in tracks.</param>
        /// <returns>List of tracks with talks.</returns>           
        public IEnumerable<Track> Organize(IList<Track> tracks, IList<Talk> talks)
        {
            var bins = tracks.SelectMany(x => x.Sessions.OfType<Session>());
            foreach (var talk in talks.OrderByDescending(x => x.Duration))
            {
                var bin = GetBestFit(bins, talk.Duration);
                if (bin != null)
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

                    bin.AddTalk(talk);
                }
            }

            return tracks;
        }

        private Session GetBestFit(IEnumerable<Session> bins, TimeSpan duration)
        {
            return bins.FirstOrDefault(b => b.Duration.TotalMinutes - b.Talks.Sum(x => x.Duration.TotalMinutes) >=  duration.TotalMinutes);
        }
    }
}

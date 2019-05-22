using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class BestFitTrackOrganizer : ITrackOrganizer
    {
        private readonly ILogger logger;
        private readonly IOptions<AppSettings> config;
        private readonly ITalkOrganizer talkOrganizer;

        public BestFitTrackOrganizer(ILogger<BestFitTrackOrganizer> logger, IOptions<AppSettings> config)
        {
            this.logger = logger;
            this.config = config;
            //this.talkOrganizer = talkOrganizer;
        }

        public void Organize(int v, IList<Talk> talks)
        {
            throw new NotImplementedException();
        }

        public void Organize(IList<Track> tracks, IList<Talk> talks)
        {
            var grouppedBy = talks.GroupBy(x => x.Duration);


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
        }

        private Session GetBestFit(IEnumerable<Session> bins, TimeSpan duration)
        {
            return bins.FirstOrDefault(b => b.Duration.TotalMinutes - b.Talks.Sum(x => x.Duration.TotalMinutes) >=  duration.TotalMinutes);
        }
    }
}

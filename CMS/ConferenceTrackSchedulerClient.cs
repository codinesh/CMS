using CMS.Model;
using CMS.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CMS
{
    public class ConferenceTrackSchedulerClient
    {
        private readonly ILogger<ConferenceTrackSchedulerClient> logger;
        private readonly ITextInputReader inputReader;
        private readonly IScheduler scheduler;
        private readonly AppSettings settings;

        public ConferenceTrackSchedulerClient(ILogger<ConferenceTrackSchedulerClient> logger, ITextInputReader inputReader, IScheduler scheduler, IOptions<AppSettings> settings)
        {
            this.logger = logger;
            this.inputReader = inputReader;
            this.scheduler = scheduler;
            this.settings = settings.Value;
        }

        public async Task Run() {
            var conferenceDetails = await GetScheduleAsync();
            DisplaySchedule(conferenceDetails);
        }

        private async Task<ConferenceDetail> GetScheduleAsync()
        {
            var talks = await inputReader.LoadTalksAsync(settings.InputFileName);
            return scheduler.Initialize(talks);
        }

        private static void DisplaySchedule(ConferenceDetail conference)
        {
            Console.WriteLine(conference.ConferenceStartDate);
            foreach (var track in conference.Tracks)
            {
                Console.WriteLine($"{track.Name}:");
                foreach (var talk in track.Sessions)
                {
                    Console.WriteLine(talk.Title);
                    Console.WriteLine(talk.ToString());
                }

                Console.WriteLine();
            }
        }
    }
}

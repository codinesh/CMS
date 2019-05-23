using CMS.Core.Exceptions;
using CMS.Model;
using CMS.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CMS
{
    /// <summary>
    /// Initialized a new instance of ConferenceTrackSchedulerClient class.
    /// </summary>
    public class ConferenceTrackSchedulerClient
    {
        private readonly ILogger<ConferenceTrackSchedulerClient> logger;
        private readonly ITextInputReader inputReader;
        private readonly IScheduler scheduler;
        private readonly AppSettings settings;

        /// <summary>
        /// Initializes new instance of ConferenceTrackSchedulerClient class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="inputReader"></param>
        /// <param name="scheduler"></param>
        /// <param name="settings"></param>
        public ConferenceTrackSchedulerClient(ILogger<ConferenceTrackSchedulerClient> logger, ITextInputReader inputReader, IScheduler scheduler, IOptions<AppSettings> settings)
        {
            this.logger = logger;
            this.inputReader = inputReader;
            this.scheduler = scheduler;
            this.settings = settings.Value;
        }

        /// <summary>
        /// Initiates the execution and generate the conference schedule.
        /// </summary>
        /// <returns></returns>
        public async Task Run() {
            try
            {
                var conferenceDetails = await GetScheduleAsync();
                DisplaySchedule(conferenceDetails);
            }
            catch (InvalidInputException ex)
            {
                logger.LogError("Talk informtion is not valid. Please update Input.");
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong. Please check the input and try again.");
            }
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

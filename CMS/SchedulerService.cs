﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS
{
    public class SchedulerService
    {
        private readonly ILogger logger;

        public SchedulerService(ILogger logger)
        {
            this.logger = logger;
        }

        public Track InitializeTrack()
        {
            logger.LogInformation("test");
            var track = new Track();
            track.Sessions = new List<Slot> {
                new Session{ Duration = new TimeSpan(0, 180, 0), SessionType = SessionType.MorningSession, Talks = new List<Talk>(), Title = "Morning Session"   },
                new Session{ Duration = new TimeSpan(0, 180, 0), SessionType = SessionType.AfternoonSession, Talks = new List<Talk>(), Title = "Afternoon Session"   }
            };

            return track;
        }
    }
}
;
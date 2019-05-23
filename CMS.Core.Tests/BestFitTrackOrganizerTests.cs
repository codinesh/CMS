using Castle.Core.Logging;
using CMS.Model;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CMS.Core.Tests
{
    public class BestFitTrackOrganizerTests
    {
        private readonly Mock<ILogger<BestFitTrackOrganizer>> loggerMock;
        private readonly BestFitTrackOrganizer organizer;
        private readonly List<Talk> talks;
        private readonly List<Track> tracks;

        public BestFitTrackOrganizerTests()
        {
            loggerMock = new Mock<ILogger<BestFitTrackOrganizer>>();
            organizer = new BestFitTrackOrganizer(loggerMock.Object);

            tracks = new List<Track>();
            tracks.Add(new Track("Track 1", new List<Slot> {
                new Session { Title = "Morning Session", StartTime = new TimeSpan(9, 0, 0), Duration = new TimeSpan(3, 0, 0) },
                new LunchBreak(new TimeSpan(3, 0, 0), new TimeSpan(12, 0, 0), "LunchBreak"),
                new Session { Title = "Afternoon Session", StartTime = new TimeSpan(13, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new NetworkingEvent(new TimeSpan(16, 0, 0))
            }));

            tracks.Add(new Track("Track 2", new List<Slot> { new Session { Title = "Morning Session", StartTime = new TimeSpan(9, 0, 0), Duration = new TimeSpan(3, 0, 0) },
                new Session { Title = "Afternoon Session", StartTime = new TimeSpan(13, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new LunchBreak(new TimeSpan(3, 0, 0), new TimeSpan(12, 0, 0), "LunchBreak"),
                new NetworkingEvent(new TimeSpan(16, 0, 0))
            }));

            talks = new List<Talk> {
                new Talk("A", 60)
                ,new Talk("B", 60)
                ,new Talk("C", 30)
                ,new Talk("D", 45)
                ,new Talk("E", 60)
                ,new Talk("F", 5)
                ,new Talk("G", 45)
                ,new Talk("H", 30)
            };
        }

        [Fact]
        public void OrganizeShouldThrowArgumentNullExceptionIfTracksIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => organizer.Organize(null, new List<Talk>()));
        }

        [Fact]
        public void OrganizeShouldThrowArgumentNullExceptionIfTalksIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => organizer.Organize(new List<Track>(), null));
        }

        [Fact]
        public void OrganizeShouldReturnListOfTrackObjectsWithProperData()
        {
            var filledTracks = organizer.Organize(tracks, talks).ToList();

            Assert.Equal(filledTracks.Count, tracks.Count);
            var track1 = filledTracks.First();

            Assert.Equal("Track 1", track1.Name);
        }
    }
}

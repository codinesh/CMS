using CMS.Model;
using System;
using Xunit;

namespace CMS.Core.Tests
{
    public class SlotFactoryTests
    {
        private readonly TimeSpan startTime;
        private readonly TimeSpan duration;
        private readonly string defaultTitle;
        private readonly string defaultCategory;

        public SlotFactoryTests()
        {
            startTime = new TimeSpan(9, 0, 0);
            duration = new TimeSpan(0, 3, 0);
            defaultTitle = "Title";
            defaultCategory = "Session";
        }

        [Fact]
        public void GetSlotShouldThrowArgumentNullExceptionIfCategoryIsNull()
        {
            Assert.Throws<ArgumentException>(() => SlotFactory.GetSlot(null, defaultTitle, startTime, duration, duration));
        }

        [Fact]
        public void GetSlotShouldThrowArgumentExceptionIfCategoryIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => SlotFactory.GetSlot("Invalid Category", defaultTitle, startTime, duration, duration));
        }

        [Fact]
        public void GetSlotShouldThrowArgumentNullExceptionIfTitleIsNull()
        {
            Assert.Throws<ArgumentException>(() => SlotFactory.GetSlot(defaultCategory, null, startTime, duration, duration));
        }

        [Fact]
        public void GetSlotShouldReturnSessionTypeForSessionCategory()
        {
            var session = SlotFactory.GetSlot(defaultCategory, defaultTitle, startTime, duration, duration);
            Assert.IsType<Session>(session);
        }

        [Fact]
        public void GetSlotShouldReturnSessionWithProperDetails()
        {
            var slot = SlotFactory.GetSlot(defaultCategory, defaultTitle, startTime, duration, duration);
            Assert.Equal(defaultTitle, slot.Title);
            Assert.Equal(startTime, slot.StartTime);
            Assert.Equal(duration, slot.Duration);
        }

        [Fact]
        public void GetSlotShouldReturnLunchBreakTypeForLunchBreakCategory()
        {
            var slot = SlotFactory.GetSlot("Lunch Break", defaultTitle, startTime, duration, duration);
            Assert.IsType<LunchBreak>(slot);
        }

        [Fact]
        public void GetSlotShouldReturnLunchBreakWithProperDetails()
        {
            var slot = SlotFactory.GetSlot("Lunch Break", defaultTitle, startTime, duration, duration);
            Assert.Equal(defaultTitle, slot.Title);
            Assert.Equal(startTime, slot.StartTime);
            Assert.Equal(duration, slot.Duration);
        }

        [Fact]
        public void GetSlotShouldReturnNetworkingEventTypeForNetworkingSessionCategory()
        {
            var slot = SlotFactory.GetSlot("Networking Session", defaultTitle, startTime, duration, duration);
            Assert.IsType<NetworkingEvent>(slot);
        }

        [Fact]
        public void GetSlotShouldReturnNetworkingEventTypeWithProperDetails()
        {
            var slot = SlotFactory.GetSlot("Networking Session", defaultTitle, startTime, duration, duration);
            var networkingEvent = slot as NetworkingEvent;
            Assert.Equal(defaultTitle, slot.Title);
            Assert.Equal(duration, networkingEvent.DefaultStartTime);
        }
    }
}

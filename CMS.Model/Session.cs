using CMS.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Model
{
    /// <summary>
    /// Implementation of Slot to represent a Session.
    /// </summary>
    public class Session : Slot
    {
        /// <summary>
        /// Returns list of Talks in this Session.
        /// </summary>
        public IList<Slot> Talks { get; }

        /// <summary>
        /// Returns type of Session.
        /// </summary>
        public SessionType SessionType { get; set; }

        /// <summary>
        /// Initializes new instance of Session class.
        /// </summary>
        public Session()
        {
            this.Talks = new List<Slot>();
        }

        /// <summary>
        /// Adds a talk object to the list of Talks at the end.
        /// </summary>
        /// <param name="talk">The Talk object to be added at the end of Talks list.</param>
        /// <returns>boolean value indicating sucecss or failure.</returns>
        /// <exceptions>throws an CanNotAddTalkException if talk can't fit into the session.</exceptions>
        public bool AddTalk(Talk talk)
        {
            var canAddTalk = RemainingUnallocatedTime() >= talk.Duration;
            if (canAddTalk)
            {
                Talks.Add(talk);
            }
            else
            {
                throw new CanNotAddTalkException("Can not add Talk, due to unavailable time.");
            }

            return canAddTalk;
        }

        /// <summary>
        /// Converts the list of Talk objects to string format.
        /// </summary>
        /// <returns>string representing values of all Talk objects.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Talks)
            {
                sb.AppendLine($"{item.StartTime} {item.Title}");
            }

            return sb.ToString();
        }

        private TimeSpan RemainingUnallocatedTime()
        {
            return new TimeSpan(0, Convert.ToInt32(this.Duration.TotalMinutes - Talks.Sum(x => x.Duration.TotalMinutes)), 0);
        }
    }
}
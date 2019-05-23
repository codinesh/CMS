using CMS.Model;
using System.Collections.Generic;

namespace CMS.Shared.Interfaces
{
    public interface IScheduler
    {
        ConferenceDetail Initialize(IList<Talk> talks);
    }
}
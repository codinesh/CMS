using CMS.Model;
using System.Collections.Generic;

namespace CMS.Shared.Interfaces
{
    public interface ITrackOrganizer
    {
        IEnumerable<Track> Organize(IList<Track> tracks, IList<Talk> talks);
    }
}
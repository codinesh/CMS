using System.Collections.Generic;

namespace CMS
{
    public interface ITrackOrganizer
    {
        void Organize(int v, IList<Talk> talks);
        void Organize(IList<Track> tracks, IList<Talk> talks);
    }
}
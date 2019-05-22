using System.Collections.Generic;

namespace CMS
{
    public interface ITrackOrganizer
    {
        void Organize(int v, List<Talk> talks);
        void Organize(List<Track> tracks, List<Talk> talks);
    }
}
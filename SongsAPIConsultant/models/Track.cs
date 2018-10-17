using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAPIConsultant.models
{
    public class Track
    {
        public string TrackId { get; set; }
        public string Artist { get; set; }
        public string Name { get; set; }
        public string Mbid { get; set; }
        public string Duration { get; set; }
        public string Listeners { get; set; }
        public string Playcount { get; set; }

        internal string GetAttributes()
        {
            return TrackId + "|" + Artist + "|" + Name + "|" + Mbid + "|" + Duration + "|" + Listeners + "|" + Playcount;
        }
    }
}

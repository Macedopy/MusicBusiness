using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicBusiness.src.Configuration.Models
{
    public class SpotifySettings
    {
        public string ClientID { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }
}
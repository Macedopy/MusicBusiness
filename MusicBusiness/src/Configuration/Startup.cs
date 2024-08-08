using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicBusiness.src.Configuration.Models;
using MusicBusiness.src.Infrastructure;

namespace MusicBusiness.Configuration
{
    public static class Startup
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("MusicBusinessConnect"));
            services.Configure<SpotifySettings>(configuration.GetSection("Spotify"));
            services.AddTransient<IUserAcessRepositorie, UserAcessRepositorie>();
        }
    }
}
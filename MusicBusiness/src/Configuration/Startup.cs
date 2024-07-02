using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicBusiness.Configuration
{
    public static class Startup
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "";
                options.ClientSecret = "";
            });
        }
    }
}
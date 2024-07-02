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
                options.ClientId = "870686917869-5kmjnrr2kcf1tr78dtj33vbb9j3mtot3.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
        }
    }
}
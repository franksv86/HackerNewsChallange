using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallengeApi.Helpers
{
    public class AppSettings
    {
        private IConfiguration Configuration { get; }
        public AppSettings(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        public string HackerNewsUrl => Configuration.GetValue<string>("HackerNewsUrl");
        public string HackerNewsName => Configuration.GetValue<string>("HackerNewsName");

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SelfWealthApiDemo
{
    public class User
    {
        
        [JsonPropertyName("login")]
        public string login { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("company")]
        public string company { get; set; }

        [JsonPropertyName("followers")]
        public long followers { get; set; }

        [JsonPropertyName("public_repos")]
        public long public_repos { get; set; }

        public double avg_followers
        {
            get
            {
                if (followers == 0 || public_repos == 0)
                {
                   return 0;
                }
                else
                {
                   return followers / public_repos;
                }
            }
        }

      
    }
}

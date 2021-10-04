using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfWealthApiDemo
{
    public class QueryParams
    {
        [FromQuery]
        public List<string> UserNames { get; set; }
    }
}

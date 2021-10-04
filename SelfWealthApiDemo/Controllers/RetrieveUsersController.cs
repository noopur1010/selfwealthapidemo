using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfWealthApiDemo
{
    [ApiController]
    [Route("[controller]")]
    public class RetrieveUsersController : ControllerBase
    {
        private readonly ILogger<RetrieveUsersController> _logger;
        private readonly IGithubApiService _githubApiService;
        private readonly ICacheUserService _cacheUserService;

        public RetrieveUsersController(ILogger<RetrieveUsersController> logger, IGithubApiService githubApiService, ICacheUserService cacheUserService)
        {
            _logger = logger;
            _githubApiService = githubApiService;
            _cacheUserService = cacheUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
                   .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            List<User> prcUsers = new List<User>();
            if (queryParams != null)
            {
                if(queryParams.UserNames != null)
                {
                    User prUser = null;
                    foreach (var userLoginName in queryParams.UserNames)
                    {
                        prUser = _cacheUserService.GetCachedUser(userLoginName).Result;
                        if (prUser == null)
                        {
                            prUser = await _githubApiService.Get(userLoginName);
                            if (prUser != null)
                            {
                                await _cacheUserService.SetCachedUser(userLoginName, prUser, cacheEntryOptions);
                            }
                        }
                        if(prUser != null)
                        {
                            prcUsers.Add(prUser);
                            prUser = null;
                        }
                    }
                }
            }

            if(prcUsers.Count == 0)
            {
                return Ok("Cannot find any users from github");
            }
            else
            {
                return Ok(prcUsers.OrderBy(x => x.login));
            }
        }
    
    }
}

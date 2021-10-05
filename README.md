# selfwealthapidemo
selfwealth api demo  

**C# task:**
* Create a net core Webapi project that has an API endpoint called retrieveUsers. This endpoint takes:
    * List<string> usernames: This is an list of usernames that will be used to look up basic information from GitHub's public API. Only users in this list should be retrieved from Github.
      * **Nupur** 
        * Create .Net Core 3.1 Web Api project with separate test project which has two endpoints to retrieve users.
        * URL: https://localhost:44360/swagger/index.html
        * URL: https://localhost:44360/retrieveusers
        * URL: https://localhost:44360/retrieveusers?UserNames=noopur1010&UserNames=gati  
* This API call returns to the user a list of basic information for those users including:
    * name
    * login
    * company
    * number of followers
    * number of public repositories
    * The average number of followers per public repository (ie. number of followers divided by the number of public repositories)
    * **Nupur** 
        * Added the business Logic as per your requirements
        * Create "User" Class which has all the required properties for the data.
* The returned users should be sorted alphabetically by name
  * **Nupur** 
    * Added the business Logic to order alphabetically     
* If some usernames cannot be found, this should not fail the other usernames that were requested
  * **Nupur** 
    * Added the business Logic to keep run the program if not found user from Cache & Github
    * URL: https://localhost:44360/RetrieveUsers?UserNames=ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff&UserNames=noopur1010
    * In the above url, you get only one user because another user not found from github
* Implement a caching layer (eg. memory cache, redis, etc) that will store a user that has been retrieved from GitHub for 2 minutes.
    * A user should be cached for 2 minutes
    * Each user should be cached individually. For example, if I request users A and B, then do another request inside 2 minutes for users B, C and D, user B should come from the cache and users C and D should come from GitHub
    * If a user is returned from the cache, it should not hit GitHub for that user
   * **Nupur** 
      * Added the business Logic into project
      * Implemented radis cache with IdestributedCache interface from Microsoft.Extensions.Caching.StackExchangeRedis
      * First find user from the Radis Cache and if cannot get the user from cache then get user from Github API.
      * To save user in Redis Cache for 2 mins.
      * After 2 mins all users deleted due to expire flag from Redis Cache.
      * If the cache has user then app get user details from Cache so there is no more https call to retrieve same data. 
* Treat this endpoint like it was going into production. Include error and exception handling and the appropriate frameworks to make the project more extensible. For example, if the app can’t connect to redis and it throws an exception, that user should be retrieved from GitHub instead
  * **Nupur** 
      * To handle error globally from Middleware exception class so we can add more login in future if require  
      * Create Log file physically which you configure path from app settings  
      * if Redis service stopped or not availble then it log exception but app gets & shows data from Github.
      * Use QueryParams custom call for FromQuery to get all parameters
        * In the future we can add another property in the class easily and get/set value through FromQuery if require
* Use regular http calls to hit GitHub's API, don’t use any pre made GitHub net core libraries to integrate with GitHub’s API
  * **Nupur** 
      * Use httpclientfactory with type client
        * To manage socket exhaustion issue
        * Not open multiple port
        * No need to restart the app If DNS or Network related configuration has been changed
        * Use this Command netstat -nao | findstr to check establish connections for more than one api calls or request
* The API endpoint needed to get Github user information is https://api.github.com/users/{username}
  * **Nupur** 
      * Used this public api to bring user related data from Github    
* Include unit tests and integration tests in the project to test the above logic. Any testing frameworks can be used for this (for example, XUnit and Moq)
    * Integration tests should be used to test your code against the live endpoints such as Redis and gitHub
    * Unit tests should be used to test your code’s logic without the external endpoint dependancies.
  * **Nupur** 
      * Created Test project as per your requirements. 
      * Use "Microsoft.AspNetCore.TestHost" to create memory server to execute the same endpoit from the main project or assembly dll file.
* Provide a Readme.md with instructions on how to execute your API endpoint
  * **Nupur** 
    * Instructions:
    * Swagger UI Available
       * So, you guys can call the retrieveusers endpoint from the UI Screen with all required query parameters.
       * URL: https://localhost:44360/swagger/index.html
    * Call retrieveusers endpoint directly and pass parameters
      * https://localhost:44360/retrieveusers?UserNames=noopur1010&UserNames=gati
    * Project Configuration & Requirements
      * Set Log path in the app settings or default log path under the base project \mylog\selfwealth_log
      * Need Redis Cache Server
      * Limited Hosted option on IIS Express or IIS server (make project enable for Linux hosting if require very eailsy)
* When submitting, zip the project including the Readme.md
      * You can download the source code from the Github 
      * https://github.com/noopur1010/selfwealthapidemo.git

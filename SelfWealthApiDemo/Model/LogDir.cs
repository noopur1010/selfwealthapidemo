using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SelfWealthApiDemo
{
    public static class LogDir
    {
        public static string GetLogFilePath(string configDirPath)
        {
            if (string.IsNullOrEmpty(configDirPath))
            {
                configDirPath = Path.Combine(Directory.GetCurrentDirectory(), "mylog"); 
            }
            configDirPath =  Path.Combine(configDirPath, "selfwealth_log-{Date}.txt");
            return configDirPath;
        }
    }
}

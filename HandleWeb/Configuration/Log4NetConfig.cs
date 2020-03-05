using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HandleWeb.Configuration
{
    public static class Log4NetConfig
    {
        public static ILoggerFactory ConfigureLog4Net(this ILoggerFactory logger)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            logger.CreateLogger(logRepository.Name.ToString());
            return logger;
        }
    }
}

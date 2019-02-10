using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Common.Infrastructure
{
    public static class CustomLoggerExtensions
    {
        public static void LogDebugAsJson(this ILogger logger, object message, params object[] args)
        {
            logger.Log(LogLevel.Debug, JsonConvert.SerializeObject(message), args);
        }
    }
}

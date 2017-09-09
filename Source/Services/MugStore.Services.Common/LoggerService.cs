namespace MugStore.Services.Common
{
    using MugStore.Data.Common;
    using MugStore.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class LoggerService : ILoggerService
    {
        private readonly IDbRepository<Log> logs;

        public LoggerService(IDbRepository<Log> logs)
        {
            this.logs = logs;
        }

        public void Log(LogLevel level, string message, string code = null)
        {
            var log = new Log()
            {
                Code = code,
                Content = message,
                Level = level
            };

            this.logs.Add(log);
            this.logs.Save();
        }

        public IEnumerable<Log> GetLogMessages()
        {
            return this.logs.All().OrderByDescending(x => x.Id).Take(100).ToList();
        }
    }
}

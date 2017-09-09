namespace MugStore.Services.Common
{
    using MugStore.Data.Models;
    using System.Collections.Generic;

    public interface ILoggerService
    {
        void Log(LogLevel level, string message, string code = null);
        IEnumerable<Log> GetLogMessages();
    }
}

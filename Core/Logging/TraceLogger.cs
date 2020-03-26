using System.Diagnostics;
using Contracts.Logging;

namespace Core.Logging
{
    public class TraceLogger : ILogger
    {
        #region Methods

        public void Log(string message)
        {
            Trace.WriteLine(message);
        }

        #endregion
    }
}

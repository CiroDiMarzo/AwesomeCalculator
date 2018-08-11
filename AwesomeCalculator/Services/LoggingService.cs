using Microsoft.SharePoint.Administration;

namespace AwesomeCalculator.Services
{
    internal class LoggingService : ILoggingService
    {
        private const string _category = "AwesomeCalculator";

        public LoggingService()
        {

        }

        public void LogInfo(string messageFormat, params object[] parameters)
        {
            SPDiagnosticsService.Local.WriteTrace(0,
                new SPDiagnosticsCategory(_category, TraceSeverity.Verbose, EventSeverity.Information),
                TraceSeverity.Verbose,
                messageFormat,
                parameters);
        }

        public void LogError(string messageFormat, params object[] parameters)
        {
            SPDiagnosticsService.Local.WriteTrace(0,
                new SPDiagnosticsCategory(_category, TraceSeverity.Unexpected, EventSeverity.Error),
                TraceSeverity.Monitorable,
                messageFormat,
                parameters);
        }
    }
}

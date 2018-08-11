using System;
namespace AwesomeCalculator.Services
{
    interface ILoggingService
    {
        void LogError(string messageFormat, params object[] parameters);
        void LogInfo(string messageFormat, params object[] parameters);
    }
}

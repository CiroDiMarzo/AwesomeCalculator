using AwesomeCalculator.Constants;
using AwesomeCalculator.Services;
using Microsoft.SharePoint;
using System;
using System.Runtime.InteropServices;

namespace AwesomeCalculator.EventReceivers
{
    [Guid("BB27C8FF-658F-48EF-9E0B-CA311ADA6F88")]
    public class SubtractionEventReceiver : SPItemEventReceiver
    {
        private readonly ILoggingService _loggingService;

        public SubtractionEventReceiver()
        {
            _loggingService = new LoggingService();
        }

        public override void ItemAdding(SPItemEventProperties properties)
        {
            SubtractNumbers(properties);

            base.ItemAdding(properties);
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            SubtractNumbers(properties);

            base.ItemUpdating(properties);
        }

        private void SubtractNumbers(SPItemEventProperties properties)
        {
            try
            {
                string sNumber1 = GetProperty(properties, Constants.Fields.Number1).ToString();
                string sNumber2 = GetProperty(properties, Constants.Fields.Number2).ToString();

                _loggingService.LogInfo("Subtracting {0} and {1}", sNumber1, sNumber2);

                properties.AfterProperties[Constants.Fields.Result] = float.Parse(sNumber1) - float.Parse(sNumber2);
            }
            catch (Exception e)
            {
                _loggingService.LogError(e.ToString());
            }
        }

        private object GetProperty(SPItemEventProperties properties, string columnName)
        {
            object obj = properties.AfterProperties[columnName];

            if (obj == null)
            {
                throw new NullReferenceException(string.Format("Could not find the column {0} in the content type.", columnName));
            }

            return obj;
        }
    }
}

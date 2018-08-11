using AwesomeCalculator.Constants;
using AwesomeCalculator.Services;
using Microsoft.SharePoint;
using System;
using System.Runtime.InteropServices;

namespace AwesomeCalculator.EventReceivers
{
    [Guid("CB246F09-B94E-4310-A23E-AD167BB3FE62")]
    public class AdditionEventReceiver : SPItemEventReceiver
    {
        private readonly ILoggingService _loggingService;

        public AdditionEventReceiver()
        {
            _loggingService = new LoggingService();
        }

        public override void ItemAdding(SPItemEventProperties properties)
        {
            AddNumbers(properties);

            base.ItemAdding(properties);
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            AddNumbers(properties);

            base.ItemUpdating(properties);
        }

        private void AddNumbers(SPItemEventProperties properties)
        {
            try
            {
                string sNumber1 = GetProperty(properties, InternalNames.Fields.Number1).ToString();
                string sNumber2 = GetProperty(properties, InternalNames.Fields.Number2).ToString();

                _loggingService.LogInfo("Adding {0} and {1}", sNumber1, sNumber2);

                properties.AfterProperties[InternalNames.Fields.Result] = float.Parse(sNumber1) + float.Parse(sNumber2);
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

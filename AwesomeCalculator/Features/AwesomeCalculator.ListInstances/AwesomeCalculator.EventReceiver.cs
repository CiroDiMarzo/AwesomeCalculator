using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using AwesomeCalculator.Services;
using AwesomeCalculator.Constants;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AwesomeCalculator.Features.AwesomeCalculator.ListInstances
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("ad233b36-630a-4680-ac84-eacdf34426b4")]
    public class AwesomeCalculatorEventReceiver : SPFeatureReceiver
    {
        private readonly ILoggingService _loggingService;
        private readonly IContentTypeService _contentTypeService;

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb;
            try
            {
                //web.AllowUnsafeUpdates = true;

                SPContentType addContentType = GetContentType(ContentTypesId.Addition, web);
                SPContentType subContentType = GetContentType(ContentTypesId.Subtraction, web);

                BindListContentType(Lists.Operations, addContentType, web);
                BindListContentType(Lists.Operations, subContentType, web);

                CreateView("Result View", Lists.Operations, new string[] { "ID", "ContentType", Fields.Number1, Fields.Number2, Fields.Result }, web);
            }
            finally
            {
                //web.AllowUnsafeUpdates = false;
            }
        }

        private void CreateView(string viewName, string listTitle, string[] viewFields, SPWeb web)
        {
            SPList list = web.Lists.TryGetList(listTitle);
            if (list != null)
            {
                StringCollection viewFieldNames = new StringCollection();
                foreach (var viewField in viewFields)
                {
                    viewFieldNames.Add(viewField);
                }
                SPViewCollection views = list.Views;
                views.Add(viewName, viewFieldNames, null, 100, true, true);
                list.Update();
                web.Update();
            }
        }

        private static void BindListContentType(string listTitle, SPContentType addContentType, SPWeb web)
        {
            SPList list = web.Lists.TryGetList(listTitle);
            if (list != null)
            {
                list.ContentTypesEnabled = true;
                list.ContentTypes.Add(addContentType);
                list.Update();
            }
        }

        private SPContentType GetContentType(string contentTypeGuid, SPWeb web)
        {
            SPContentTypeId contentTypeId = new SPContentTypeId(contentTypeGuid);
            SPContentType contentType = web.ContentTypes[contentTypeId];
            return contentType;
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}

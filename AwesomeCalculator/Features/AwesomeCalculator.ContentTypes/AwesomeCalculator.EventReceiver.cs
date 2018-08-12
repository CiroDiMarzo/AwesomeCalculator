using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using AwesomeCalculator.Constants;

namespace AwesomeCalculator.Features.AwesomeCalculator.ContentTypes
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("38f66512-30a9-4961-bb37-f6e20fdc392d")]
    public class AwesomeCalculatorEventReceiver : SPFeatureReceiver
    {
        private readonly string _additionEventReceiverClassName = "AwesomeCalculator.EventReceivers.AdditionEventReceiver";
        private readonly string _subtractionEventReceiverClassName = "AwesomeCalculator.EventReceivers.SubtractionEventReceiver";
        private readonly string _assemblyName = "AwesomeCalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6243f69a1bdc7625";
        
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;
            SPWeb web = site.OpenWeb();

            try
            {
                //web.AllowUnsafeUpdates = true;
                SPContentType addContentType = BindEventReceiver(ContentTypesId.Addition, _additionEventReceiverClassName, web);
                SPContentType subContentType = BindEventReceiver(ContentTypesId.Subtraction, _subtractionEventReceiverClassName, web);

                DeleteFieldLink(addContentType, new string[] { "Title" }, web);
                DeleteFieldLink(subContentType, new string[] { "Title" }, web);
            }
            finally
            {
                //web.AllowUnsafeUpdates = false;
                web.Dispose();
            }
        }

        private SPContentType BindEventReceiver(string contentTypeGuid, string eventClassName, SPWeb web)
        {
            SPContentTypeId contentTypeId = new SPContentTypeId(contentTypeGuid);
            SPContentType contentType = web.ContentTypes[contentTypeId];

            // adds the event receiver to the sit content type so that the children list content types will inherit the binding
            BindEventReceiver(contentType, eventClassName, SPEventReceiverType.ItemAdding);
            BindEventReceiver(contentType, eventClassName, SPEventReceiverType.ItemUpdating);
            return contentType;
        }

        private void BindEventReceiver(SPContentType contentType, string eventClassName, SPEventReceiverType eventType)
        {
            SPEventReceiverDefinition eventReceiver = contentType.EventReceivers.Add();
            eventReceiver.Class = eventClassName;
            eventReceiver.Assembly = _assemblyName;
            eventReceiver.Type = eventType;
            eventReceiver.Data = string.Empty;
            eventReceiver.Update();
            contentType.Update(true);
        }

        private void DeleteFieldLink(string contentTypeGuid, string[] fieldNames, SPWeb web)
        {
            SPContentTypeId contentTypeId = new SPContentTypeId(contentTypeGuid);
            SPContentType contentType = web.ContentTypes[contentTypeId];

            DeleteFieldLink(contentType, fieldNames, web);
        }

        private void DeleteFieldLink(SPContentType contentType, string[] fieldNames, SPWeb web)
        {
            foreach (var fieldName in fieldNames)
            {
                contentType.FieldLinks.Delete(fieldName);
            }
            contentType.Update();
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

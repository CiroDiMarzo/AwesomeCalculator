using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using AwesomeCalculator.Services;
using AwesomeCalculator.Constants;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls.WebParts;

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

                try
                {
                    AddWebPartToHome();
                }
                catch (Exception e)
                {
                    _loggingService.LogError(e.ToString());
                }
            }
            finally
            {
                //web.AllowUnsafeUpdates = false;
            }
        }

        private void AddWebPartToHome()
        {
            SPWeb web = SPContext.Current.Web;
            SPFile file = web.GetFile("SitePages/Home.aspx");
            
            using (SPLimitedWebPartManager webPartManager = file.GetLimitedWebPartManager(PersonalizationScope.Shared))
            {
                string zoneId = ClearWebParts(webPartManager);

                using (System.Web.UI.WebControls.WebParts.WebPart calculatorWebPart = GetWebPart(web, "AwesomeCalculator_CalculatorWebPart.webpart"))
                {
                    calculatorWebPart.ChromeType = PartChromeType.TitleOnly;
                    calculatorWebPart.Title = "Calculator";
                    webPartManager.AddWebPart(calculatorWebPart, "Left", 1);
                    webPartManager.SaveChanges(calculatorWebPart);
                }
            }
        }

        private string ClearWebParts(SPLimitedWebPartManager webPartManager)
        {
            string zoneId = "Bottom";
            var webPartList = new List<System.Web.UI.WebControls.WebParts.WebPart>();

            foreach (System.Web.UI.WebControls.WebParts.WebPart webpart in webPartManager.WebParts)
            {
                webPartList.Add(webpart);
            }

            if (webPartList.Any())
            {
                // take any of the webpart zones Id
                zoneId = webPartManager.GetZoneID(webPartList[0]);

                foreach (var webpart in webPartList)
                {
                    webPartManager.DeleteWebPart(webpart);
                }
            }
            return zoneId;
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
            SPContentType contentType = web.Site.RootWeb.ContentTypes[contentTypeId];
            return contentType;
        }

        public static System.Web.UI.WebControls.WebParts.WebPart GetWebPart(SPWeb web, string webPartName)
        {
            var query = new SPQuery();
            query.Query = String.Format("<Where><Eq><FieldRef Name='FileLeafRef'/><Value Type='File'>{0}</Value></Eq></Where>", webPartName);

            SPList webPartGallery;
            if (web.IsRootWeb)
            {
                webPartGallery = web.GetCatalog(SPListTemplateType.WebPartCatalog);
            }
            else
            {
                webPartGallery = web.ParentWeb.GetCatalog(SPListTemplateType.WebPartCatalog);
            }
            var webParts = webPartGallery.GetItems(query);
            var typeName = webParts[0].GetFormattedValue("WebPartTypeName");
            var assemblyName = webParts[0].GetFormattedValue("WebPartAssembly");
            var webPartHandle = Activator.CreateInstance(
                assemblyName, typeName);

            var webPart = (System.Web.UI.WebControls.WebParts.WebPart)webPartHandle.Unwrap();
            return webPart;
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

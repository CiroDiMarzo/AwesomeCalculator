using System;
namespace AwesomeCalculator.Services
{
    interface IContentTypeService
    {
        bool TryBind(string contentTypeId, string listTitle, Microsoft.SharePoint.SPWeb currentWeb);
        void Unbind(string contentTypeTitle, string listTitle, Microsoft.SharePoint.SPWeb currentWeb);
    }
}

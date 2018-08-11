using Microsoft.SharePoint;
using System;

namespace AwesomeCalculator.Services
{
    internal class ContentTypeService : IContentTypeService
    {
        private readonly ILoggingService _loggingService;

        public ContentTypeService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public bool TryBind(string contentTypeId, string listTitle, SPWeb currentWeb)
        {
            if (string.IsNullOrEmpty(contentTypeId))
                throw new ArgumentException("contentTypeId");

            if (string.IsNullOrEmpty(listTitle))
                throw new ArgumentException("listTitle");

            if (currentWeb == null)
                throw new ArgumentNullException("currentWeb");

            SPList list = currentWeb.Lists.TryGetList(listTitle);

            bool result = false;

            if (list != null)
            {
                var spContentTypeId = new SPContentTypeId(contentTypeId);
                var contentType = currentWeb.ContentTypes[spContentTypeId];

                if (contentType != null)
                {
                    if (list.ContentTypes[contentType.Name] != null)
                    {
                        list.ContentTypes.Add(contentType);
                        list.Update();
                        result = true;
                    }
                }
            }

            return result;
        }

        public void Unbind(string contentTypeTitle, string listTitle, SPWeb currentWeb)
        {
            if (string.IsNullOrEmpty(contentTypeTitle))
                throw new ArgumentException("contentTypeTitle");

            if (string.IsNullOrEmpty(listTitle))
                throw new ArgumentException("listTitle");

            if (currentWeb == null)
                throw new ArgumentNullException("currentWeb");
        }
    }
}

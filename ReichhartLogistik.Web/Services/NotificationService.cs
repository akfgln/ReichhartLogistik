using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using ReichhartLogistik.Web.Models;

namespace ReichhartLogistik.Web.Services
{
    public class NotificationService : INotificationService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotificationService(IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        protected virtual void PrepareTempData(string key, string message, bool encode = true)
        {
            var context = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(context);
            var nMessage = new NotifyModel
            {
                Message = message,
                isHtmlEncode = encode
            };

            tempData[key] = JsonConvert.SerializeObject(nMessage);
        }

        public virtual void SuccessNotification(string message, bool encode = true)
        {
            PrepareTempData("Success", message, encode);
        }
        public virtual void WarningNotification(string message, bool encode = true)
        {
            PrepareTempData("Warning", message, encode);
        }
        public virtual void ErrorNotification(string message, bool encode = true)
        {
            PrepareTempData("Error", message, encode);
        }
    }
}

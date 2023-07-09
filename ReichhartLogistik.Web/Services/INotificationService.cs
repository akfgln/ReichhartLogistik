namespace ReichhartLogistik.Web.Services
{
    public interface INotificationService
    {
        void SuccessNotification(string message, bool encode = true);

        void WarningNotification(string message, bool encode = true);

        void ErrorNotification(string message, bool encode = true);
    }
}

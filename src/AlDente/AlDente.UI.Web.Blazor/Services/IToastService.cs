using Syncfusion.Blazor.Notifications;

namespace AlDente.UI.Web.Blazor.Services
{
    public interface IToastService
    {
        SfToast SfToast { get; set; }

        void ShowMessage(MessageType type, string title, string content = null);
    }

    public class ToastService : IToastService
    {
        public SfToast SfToast { get; set; }

        public void ShowMessage(MessageType type, string title, string content = null)
        {
            string cssClass = null;
            string icon = null;
            switch (type)
            {
                case MessageType.Success:
                    cssClass = "e-toast-success";
                    icon = "e-success toast-icons";
                    break;
                case MessageType.Error:
                    cssClass = "e-toast-danger";
                    icon = "e-error toast-icons";
                    break;
                case MessageType.Warning:
                    cssClass = "e-toast-warning";
                    icon = "e-warning toast-icons";
                    break;
                case MessageType.Info:
                    cssClass = "e-toast-info";
                    icon = "e-info toast-icons";
                    break;
                default:
                    break;
            }
            SfToast.Show(new ToastModel
            {
                Title = title,
                Content = content,
                CssClass = cssClass,
                Icon = icon
            });
        }
    }

    public enum MessageType
    {
        Success,
        Error,
        Warning,
        Info
    }
}

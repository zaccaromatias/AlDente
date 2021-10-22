

namespace AlDente.UI.Web.Blazor.Services
{
    public class ToastOption
    {
        public int Timeout { get; set; }
        public bool ShowCloseButton { get; set; }



        public ToastOption()
        {
            this.Timeout = 5000;
            this.ShowCloseButton = false;
        }
    }
    public interface IToastService
    {
        Syncfusion.Blazor.Notifications.SfToast SfToast { get; set; }

        void ShowMessage(MessageType type, string title, string content = null, ToastOption toastOption = null);
    }

    public class ToastService : IToastService
    {
        public Syncfusion.Blazor.Notifications.SfToast SfToast { get; set; }

        public void ShowMessage(MessageType type, string title, string content = null, ToastOption toastOption = null)
        {
            toastOption = toastOption ?? new ToastOption();
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


            var model = new Syncfusion.Blazor.Notifications.ToastModel
            {
                Title = title,
                Content = content,
                CssClass = cssClass,
                Icon = icon,
                Timeout = toastOption.Timeout,
                ShowCloseButton = toastOption.ShowCloseButton

            };
            SfToast.Show(model);
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

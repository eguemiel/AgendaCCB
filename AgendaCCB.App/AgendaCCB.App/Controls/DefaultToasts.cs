using Acr.UserDialogs;
using System;
using System.Drawing;

namespace AgendaCCB.App.Controls
{
    public static class DefaultToasts
    {
        public static void Error(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.SetBackgroundColor(Color.FromArgb(255, 115, 115));
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Info(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.SetBackgroundColor(Color.FromArgb(33, 150, 243));
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Error(string mensagem, TimeSpan timeout)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.SetDuration(timeout);
            toastConfig.SetBackgroundColor(Color.FromArgb(255, 115, 115));
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Info(string mensagem, TimeSpan timeout)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.SetDuration(timeout);
            toastConfig.SetBackgroundColor(Color.FromArgb(33, 150, 243));
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Success(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);            
            toastConfig.SetBackgroundColor(Color.FromArgb(92, 184, 92));
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}

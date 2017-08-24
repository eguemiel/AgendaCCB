using Acr.UserDialogs;
using System;
using System.Drawing;

namespace AgendaCCB.App.Controls
{
    public static class DefaultToasts
    {
        public static void Erro(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.BackgroundColor = Color.FromArgb(255, 115, 115);
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Informacao(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.BackgroundColor = Color.FromArgb(33, 150, 243);
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Erro(string mensagem, TimeSpan timeout)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.SetDuration(timeout);
            toastConfig.BackgroundColor = Color.FromArgb(255, 115, 115);
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void Informacao(string mensagem, TimeSpan timeout)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);
            toastConfig.SetDuration(timeout);
            toastConfig.BackgroundColor = Color.FromArgb(33, 150, 243);
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}

using System;
using Xamarin.Forms;

namespace AgendaCCB.App.Views
{
    public partial class CongregationPhonePage : ContentPage
    {
        public CongregationPhonePage()
        {
            InitializeComponent();
            LblClickFunc();
        }

        void LblClickFunc()
        {
            lblFaxCongregation.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked(lblFaxCongregation.Text)));
            lblPhoneCongregation.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked(lblPhoneCongregation.Text)));
        }

        private void OnLabelClicked(string phone)
        {
            Device.OpenUri(new Uri(string.Format("tel:{0}", phone)));
        }
    }
}

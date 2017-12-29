using AgendaCCB.App.Models;
using System;
using Xamarin.Forms;

namespace AgendaCCB.App.Views
{
    public partial class CollaboratorPage : ContentPage
    {
        public CollaboratorPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var phoneNumber = (PhoneNumber)e.Item;            
            Device.OpenUri(new Uri(string.Format("tel:{0}", phoneNumber.Number)));
        }
    }
}

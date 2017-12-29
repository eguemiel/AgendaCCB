using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AgendaCCB.App.Controls
{
    public class LabelClickable : Label
    {
        public static readonly BindableProperty ItemClickedCommandProperty =
          BindableProperty.Create("ItemClickedCommand",
                            typeof(ICommand),
                            typeof(Label),
                            null);
        public ICommand ItemClickedCommand
        {
            get { return (ICommand)GetValue(ItemClickedCommandProperty); }
            set
            {
                SetValue(ItemClickedCommandProperty, value);
            }
        }

        public LabelClickable()
        {
            Initialize();
        }

        private void Initialize()
        {
            TapGestureRecognizer singleTap = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1
            };

            this.GestureRecognizers.Add(singleTap);

            singleTap.Tapped += SingleTap_Tapped;
        }

        private void SingleTap_Tapped(object sender, EventArgs e)
        {
            if (ItemClickedCommand != null && ItemClickedCommand.CanExecute(null))
                ItemClickedCommand.Execute(null);
        }
    }
}

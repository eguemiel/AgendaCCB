using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace AgendaCCB.App.Controls
{
    public class ListView : Xamarin.Forms.ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
          BindableProperty.Create("ItemTappedCommand",
                            typeof(ICommand),
                            typeof(ListView),
                            null);
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set
            {
                SetValue(ItemTappedCommandProperty, value);
            }
        }

        public static readonly BindableProperty InfiniteScrollCommandProperty =
            BindableProperty.Create("InfiniteScrollCommand",
                    typeof(ICommand),
                    typeof(ListView),
                    null);
        public ICommand InfiniteScrollCommand
        {
            get { return (ICommand)GetValue(InfiniteScrollCommandProperty); }
            set
            {
                SetValue(InfiniteScrollCommandProperty, value);
            }
        }

        public static readonly BindableProperty ScrollToTopProperty =
            BindableProperty.Create("ScrollToTop",
                    typeof(bool),
                    typeof(ListView),
                    false,
                    BindingMode.TwoWay,
                    propertyChanged: (bindable, oldValue, newValue) =>
                    {
                        ((ListView)bindable).ScrollToTop = false;

                    }
                    );


        public bool ScrollToTop
        {
            get { return (bool)GetValue(ScrollToTopProperty); }
            set
            {
                SetValue(ScrollToTopProperty, value);
            }
        }

        //Alternative
        static ListViewCachingStrategy GetStrategy()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return ListViewCachingStrategy.RetainElement;
            }
            return ListViewCachingStrategy.RecycleElement;
        }


        public ListView() : base(GetStrategy())
        {
            Initialize();
        }

        private void Initialize()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
            ItemTapped += ListView_ItemTapped;

            ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };

        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ItemTappedCommand != null && ItemTappedCommand.CanExecute(null))
                ItemTappedCommand.Execute(e.Item);
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var lv = ((ListView)sender);
            if (lv.IsRefreshing)
                return;

            var items = ItemsSource as IList;
            if (items != null && e.Item == items[items.Count - 1])
            {
                if (InfiniteScrollCommand != null && InfiniteScrollCommand.CanExecute(null))
                    InfiniteScrollCommand.Execute(null);
            }
        }
    }
}

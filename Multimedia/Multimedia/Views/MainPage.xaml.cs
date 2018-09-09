using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.Collections.ObjectModel;
using MenuItem = Multimedia.Models.MenuItem;

namespace Multimedia.Views
{
    public partial class MainPage : MasterDetailPage
    {
        ObservableCollection<MenuItem> MenuItems = new ObservableCollection<MenuItem>();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new
            {
                Logo = "logo_multimedia.jpg",
            };

            MenuItems.Add(new MenuItem { Title = "Home", Icon = "ic_home.png", TargetType = typeof(HomePage) });
            MenuItems.Add(new MenuItem { Title = "Plano de Estudos", Icon = "ic_book.png", TargetType = typeof(SubjectsPage) });
            MenuItems.Add(new MenuItem { Title = "Eventos", Icon = "ic_event_available.png", TargetType = typeof(EventsPage) });
            MenuItems.Add(new MenuItem { Title = "Avisos", Icon = "ic_notifications_active.png", TargetType = typeof(NoticesPage) });
            MenuItems.Add(new MenuItem { Title = "Propostas", Icon = "ic_work.png", TargetType = typeof(OffersPage) });


            MenuItemsViews.ItemsSource = MenuItems;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
        }

        private void MenuItemsViews_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}

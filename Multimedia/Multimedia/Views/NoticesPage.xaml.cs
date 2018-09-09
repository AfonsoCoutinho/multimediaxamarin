using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Multimedia.Models;
using Multimedia.ViewModels;

namespace Multimedia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticesPage : ContentPage
    {
        public NoticesPage()
        {
            InitializeComponent();
            BindingContext = new NoticeViewModel();
            ((NoticeViewModel)BindingContext).GetNoticesComand.Execute(null);
        }

        private async void ListViewNotices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var selectedNotice = (Notice)e.SelectedItem;
            await Navigation.PushAsync(new NoticeDetailPage(selectedNotice.Id));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
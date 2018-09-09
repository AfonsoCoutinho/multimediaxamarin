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
    public partial class OffersPage : ContentPage
    {
        public OffersPage()
        {
            InitializeComponent();
            BindingContext = new OfferViewModel();
            ((OfferViewModel)BindingContext).GetOffersComand.Execute(null);
        }

        private async void ListViewOffers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var selectedOffer = (Offer)e.SelectedItem;
            await Navigation.PushAsync(new OfferDetailPage(selectedOffer.Id));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Multimedia.ViewModels;

namespace Multimedia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferDetailPage : ContentPage
    {
        public OfferDetailPage(int Id)
        {
            InitializeComponent();
            BindingContext = new OfferViewModel { SelectedId = Id };
            ((OfferViewModel)BindingContext).GetOfferComand.Execute(null);
        }
    }
}
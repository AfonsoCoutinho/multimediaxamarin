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
	public partial class EventsPage : ContentPage
	{
		public EventsPage ()
		{
			InitializeComponent ();
            BindingContext = new EventViewModel();
            ((EventViewModel)BindingContext).GetEventsComand.Execute(null);
        }

        private async void ListViewEvents_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var selectedEvent = (Event)e.SelectedItem;
            await Navigation.PushAsync(new EventDetailPage(selectedEvent.Id));
            ((ListView)sender).SelectedItem = null;
        }
	}
}
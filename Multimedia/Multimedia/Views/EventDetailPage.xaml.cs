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
	public partial class EventDetailPage : ContentPage
	{
		public EventDetailPage (int Id)
		{
			InitializeComponent ();
            BindingContext = new EventViewModel { SelectedId= Id };
            ((EventViewModel)BindingContext).GetEventComand.Execute(null);
        }
	}
}
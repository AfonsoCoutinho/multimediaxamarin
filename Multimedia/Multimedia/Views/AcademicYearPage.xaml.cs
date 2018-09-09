using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Multimedia.ViewModels;

namespace Multimedia.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AcademicYearPage : ContentPage
	{
        public bool IsLoad = false;

		public AcademicYearPage (int year)
		{
			InitializeComponent ();
            BindingContext = new AcademicYearViewModel() { Year = year };
        }

        public void LoadTab() {
            if (!IsLoad) {
                ((AcademicYearViewModel)BindingContext).GetSubjectsComand.Execute(null);
                IsLoad = true;
            }
        }
    }
}
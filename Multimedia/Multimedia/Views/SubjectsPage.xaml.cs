using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Multimedia.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubjectsPage : TabbedPage
	{
        Page currentPage;
		public SubjectsPage ()
		{
			InitializeComponent ();

            Children.Add(new AcademicYearPage(1));
            Children.Add(new AcademicYearPage(2));
            Children.Add(new AcademicYearPage(3));

            currentPage = Children[0];
            ((AcademicYearPage)currentPage).LoadTab();

            CurrentPageChanged += Handle_CurrentPageChanged;
        }

        void Handle_CurrentPageChanged(object sender, EventArgs e)
        {
            currentPage = CurrentPage;
            ((AcademicYearPage)currentPage).LoadTab();
        }
    }
}
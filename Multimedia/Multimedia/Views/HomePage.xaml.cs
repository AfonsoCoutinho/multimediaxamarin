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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new CourseViewModel();
            ((CourseViewModel)BindingContext).GetCourseCommand.Execute(null);

            //foreach (Objective objective in ((CourseViewModel)BindingContext).SelectedCourse.Objectives)
            //{
            //    ListObjectives.Children.Add(new Label { Text = objective.Description });
            //}
            //for (int i = 0; i< 10; i++)
            //{
            //    ListObjectives.Children.Add(new Label { Text = $"• Option {i}"});
            //}
            //ListObjectives.HeightRequest = 10 * 30;
        }
	}
}
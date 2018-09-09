using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Multimedia.Models;
using Multimedia.Clients;
using Xamarin.Forms;

namespace Multimedia.ViewModels
{
    class CourseViewModel : ViewModelBase
    {
        public Course SelectedCourse { get; set; }

        public CourseViewModel() : base()
        {
            _GetCourseCommand = new Command(async () => await GetCourseCommandExecute());
        }

        // Command
        private Command _GetCourseCommand;
        public Command GetCourseCommand => _GetCourseCommand ?? 
            (_GetCourseCommand = new Command(
                async () => await GetCourseCommandExecute(), () => IsNotBusy));

        async Task GetCourseCommandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetCourseHttpClient.Current.GetCourse();
                var Objectives = new List<Objective>();
                foreach (GetCourseHttpClient.ObjectiveResult obj in result.objectives)
                {
                    Objectives.Add(new Objective { Description = obj.description });
                }
                var Careers = new List<Career>();
                foreach (GetCourseHttpClient.CareerResult obj in result.careers)
                {
                    Careers.Add(new Career { Description = obj.description });
                }
                var Contacts = new List<Contact>();
                foreach (GetCourseHttpClient.ContactResult obj in result.contacts)
                {
                    Contacts.Add(new Contact {
                        Type = obj.type,
                        Name = obj.name,
                        Email = obj.email,
                    });
                }
                SelectedCourse = new Course
                {
                    Name = result.name,
                    Description = result.description,
                    ObjectivesDescription = result.objectives_description,
                    Objectives = Objectives,
                    CareersDescription = result.careers_description,
                    Careers = Careers,
                    Contacts = Contacts,
                };

                OnPropertyChanged(nameof(SelectedCourse));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetCourseCommand.ChangeCanExecute();
            }
        }
    }
}

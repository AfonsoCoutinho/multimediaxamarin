using System;
using System.Collections.Generic;
using System.Text;

using Multimedia.Clients;
using Multimedia.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Multimedia.ViewModels
{
    class AcademicYearViewModel : ViewModelBase
    {
        public int Year { get; set; }
        public ObservableCollection<Subject> SubjectsFirstSemester { get; set; }
        public ObservableCollection<Subject> SubjectsSecondSemester { get; set; }

        public AcademicYearViewModel() : base()
        {
            SubjectsFirstSemester = new ObservableCollection<Subject>();
            SubjectsSecondSemester = new ObservableCollection<Subject>();
            _GetSubjectsComand = new Command(async () => await GetSubjectsComandExecute());


        }

        public string TabName
        {
            get { return $"{Year}º Ano"; }
        }

        private Command _GetSubjectsComand;
        public Command GetSubjectsComand => _GetSubjectsComand ?? (_GetSubjectsComand = new Command(async () => await GetSubjectsComandExecute(), () => IsNotBusy));


        async Task GetSubjectsComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetSubjectsHttpClient.Current.GetSubjects(Year);

                foreach(SubjectResult res in result)
                {
                    if(Convert.ToInt32(res.semester)%2 == 1)
                    {
                        SubjectsFirstSemester.Add(new Subject { Name = res.subject, ECTS = Convert.ToInt32(res.ects) });
                    } else
                    {
                        SubjectsSecondSemester.Add(new Subject { Name = res.subject, ECTS = Convert.ToInt32(res.ects) });
                    }

                }

                OnPropertyChanged(nameof(SubjectsFirstSemester));
                OnPropertyChanged(nameof(SubjectsSecondSemester));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetSubjectsComand.ChangeCanExecute();
            }
        }
    }
}

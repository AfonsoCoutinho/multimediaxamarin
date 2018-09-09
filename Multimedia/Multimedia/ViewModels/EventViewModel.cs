using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Multimedia.Models;
using Multimedia.Clients;

namespace Multimedia.ViewModels
{
    class EventViewModel : ViewModelBase
    {
        public ObservableCollection<Event> Events { get; set; }
        public Event SelectedEvent { get; set; }

        private int _SelectedId = 0;
        public int SelectedId
        {
            get => _SelectedId;
            set
            {
                _SelectedId = value;
                OnPropertyChanged();
            }
        }


        public EventViewModel() : base()
        {
            Events = new ObservableCollection<Event>();
            _GetEventComand = new Command(async () => await GetEventComandExecute());
            _GetEventsComand = new Command(async () => await GetEventsComandExecute());
        }

        // Gets

        public string EventName
        {
            get {
                return SelectedEvent != null ? SelectedEvent.Name : "";
            }
        }
        public string EventDescription
        {
            get
            {
                return SelectedEvent != null ? SelectedEvent.Description : "";
            }
        }
        public string EventDate
        {
            get
            {
                return SelectedEvent != null ? SelectedEvent.Date : "";
            }
        }
        public string EventType
        {
            get
            {
                return SelectedEvent != null ? SelectedEvent.Type : "";
            }
        }


        // Commands 

        private Command _GetEventsComand;
        public Command GetEventsComand => _GetEventsComand ?? (_GetEventsComand = new Command(async () => await GetEventsComandExecute(), () => IsNotBusy));


        async Task GetEventsComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetEventsHttpClient.Current.GetEvents();

                foreach (EventResult res in result)
                {
                    Events.Add(new Event { Id = Convert.ToInt32(res.id), Name = res.name, Description = res.description, Date = res.date, Type = res.type });
                }

                // Debug
                // Events.Add(new Event { Name = '', Description = '' });


                OnPropertyChanged(nameof(Events));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetEventsComand.ChangeCanExecute();
            }
        }


        private Command _GetEventComand;
        public Command GetEventComand => _GetEventComand ?? (_GetEventComand = new Command(async () => await GetEventComandExecute(), () => IsNotBusy));


        async Task GetEventComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetEventsHttpClient.Current.GetEvent(_SelectedId);

                SelectedEvent = new Event { Id = Convert.ToInt32(result.id), Name = result.name, Description = result.description, Date = result.date, Type = result.type };


                // Debug
                // SelectedEvent = new Event { Name = '', Description = '' });


                OnPropertyChanged(nameof(SelectedEvent));
                OnPropertyChanged(nameof(EventName));
                OnPropertyChanged(nameof(EventDescription));
                OnPropertyChanged(nameof(EventDate));
                OnPropertyChanged(nameof(EventType));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetEventsComand.ChangeCanExecute();
            }
        }

    }
}

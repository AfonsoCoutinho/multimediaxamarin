using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Multimedia.Models;
using Multimedia.Clients;

namespace Multimedia.ViewModels
{
    class NoticeViewModel : ViewModelBase
    {
        public ObservableCollection<Notice> Notices { get; set; }
        public Notice SelectedNotice { get; set; }

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


        public NoticeViewModel() : base()
        {
            Notices = new ObservableCollection<Notice>();
            _GetNoticeComand = new Command(async () => await GetNoticeComandExecute());
            _GetNoticesComand = new Command(async () => await GetNoticesComandExecute());
        }

        // Gets

        public string NoticeName
        {
            get
            {
                return SelectedNotice != null ? SelectedNotice.Name : "";
            }
        }
        public string NoticeDescription
        {
            get
            {
                return SelectedNotice != null ? SelectedNotice.Description : "";
            }
        }
        public string NoticeDate
        {
            get
            {
                return SelectedNotice != null ? SelectedNotice.Date : "";
            }
        }
        public string NoticeType
        {
            get
            {
                return SelectedNotice != null ? SelectedNotice.Type : "";
            }
        }


        // Commands 

        private Command _GetNoticesComand;
        public Command GetNoticesComand => _GetNoticesComand ?? (_GetNoticesComand = new Command(async () => await GetNoticesComandExecute(), () => IsNotBusy));


        async Task GetNoticesComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetNoticesHttpClient.Current.GetNotices();

                foreach (NoticeResult res in result)
                {
                    Notices.Add(new Notice { Id = Convert.ToInt32(res.id), Name = res.name, Description = res.description, Date = res.date, Type = res.type });
                }

                // Debug
                // Events.Add(new Event { Name = '', Description = '' });


                OnPropertyChanged(nameof(Notices));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetNoticesComand.ChangeCanExecute();
            }
        }


        private Command _GetNoticeComand;
        public Command GetNoticeComand => _GetNoticeComand ?? (_GetNoticeComand = new Command(async () => await GetNoticeComandExecute(), () => IsNotBusy));


        async Task GetNoticeComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetNoticesHttpClient.Current.GetNotice(_SelectedId);

                SelectedNotice = new Notice { Id = Convert.ToInt32(result.id), Name = result.name, Description = result.description, Date = result.date, Type = result.type };


                // Debug
                // SelectedEvent = new Event { Name = '', Description = '' });


                OnPropertyChanged(nameof(SelectedNotice));
                OnPropertyChanged(nameof(NoticeName));
                OnPropertyChanged(nameof(NoticeDescription));
                OnPropertyChanged(nameof(NoticeDate));
                OnPropertyChanged(nameof(NoticeType));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetNoticesComand.ChangeCanExecute();
            }
        }

    }
}

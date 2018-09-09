using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Multimedia.Models;
using Multimedia.Clients;

namespace Multimedia.ViewModels
{
    class OfferViewModel : ViewModelBase
    {
        public ObservableCollection<Offer> Offers { get; set; }
        public Offer SelectedOffer { get; set; }

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


        public OfferViewModel() : base()
        {
            Offers = new ObservableCollection<Offer>();
            _GetOfferComand = new Command(async () => await GetOfferComandExecute());
            _GetOffersComand = new Command(async () => await GetOffersComandExecute());
        }

        // Gets

        public string OfferName
        {
            get
            {
                return SelectedOffer != null ? SelectedOffer.Name : "";
            }
        }
        public string OfferDescription
        {
            get
            {
                return SelectedOffer != null ? SelectedOffer.Description : "";
            }
        }
        public string OfferDate
        {
            get
            {
                return SelectedOffer != null ? SelectedOffer.Date : "";
            }
        }
        public string OfferType
        {
            get
            {
                return SelectedOffer != null ? SelectedOffer.Type : "";
            }
        }


        // Commands 

        private Command _GetOffersComand;
        public Command GetOffersComand => _GetOffersComand ?? (_GetOffersComand = new Command(async () => await GetOffersComandExecute(), () => IsNotBusy));


        async Task GetOffersComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetOffersHttpClient.Current.GetOffers();

                foreach (OfferResult res in result)
                {
                    Offers.Add(new Offer { Id = Convert.ToInt32(res.id), Name = res.name, Description = res.description, Date = res.date, Type = res.type });
                }

                // Debug
                // Events.Add(new Event { Name = '', Description = '' });


                OnPropertyChanged(nameof(Offers));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetOffersComand.ChangeCanExecute();
            }
        }


        private Command _GetOfferComand;
        public Command GetOfferComand => _GetOfferComand ?? (_GetOfferComand = new Command(async () => await GetOfferComandExecute(), () => IsNotBusy));


        async Task GetOfferComandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                var result = await GetOffersHttpClient.Current.GetOffer(_SelectedId);

                SelectedOffer = new Offer {
                    Id = Convert.ToInt32(result.id),
                    Name = result.name,
                    Description = result.description,
                    Date = result.date,
                    Type = result.type
                };


                // Debug
                // SelectedEvent = new Event { Name = '', Description = '' });


                OnPropertyChanged(nameof(SelectedOffer));
                OnPropertyChanged(nameof(OfferName));
                OnPropertyChanged(nameof(OfferDescription));
                OnPropertyChanged(nameof(OfferDate));
                OnPropertyChanged(nameof(OfferType));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops!!!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                GetOffersComand.ChangeCanExecute();
            }
        }

    }
}

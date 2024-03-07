using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationRenovationOverviewViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private ObservableCollection<AccommodationRenovation> accommodationRenovations;
        public ObservableCollection<AccommodationRenovation> AccommodationRenovations
        {
            get { return accommodationRenovations; }
            set
            {
                accommodationRenovations = value;
                OnPropertyChanged();
            }
        }

        public AccommodationRenovationController _accommodationRenovationController;

        private AccommodationRenovation _accommodationRenovation;
        public AccommodationRenovation SelectedAccommodationRenovation 
        { get { return _accommodationRenovation;  }

            set
            {
                _accommodationRenovation = value;
                OnPropertyChanged();
            }
        }

        public Accommodation SelectedAccommodation { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand CancelRenovationCommand { get; set; }
        public AccommodationRenovationOverviewViewModel(Accommodation accommodation)
        {
            _accommodationRenovationController = new AccommodationRenovationController();

            SelectedAccommodation = accommodation;

            AccommodationRenovations = new ObservableCollection<AccommodationRenovation>(_accommodationRenovationController.GetAllValidRenovations(accommodation));
            
            CloseCommand = new RelayCommand(Execute_CloseCommand);
            CancelRenovationCommand = new RelayCommand(Execute_CancelRenovationCommand, CanExecute_CancelRenovationCommand);
        }

        public void Execute_CloseCommand(object param)
        {
            Close();
        }

        public void Refresh()
        {
            AccommodationRenovations.Clear();
            foreach (AccommodationRenovation renovation in _accommodationRenovationController.GetAllValidRenovations(SelectedAccommodation)) 
            {
                AccommodationRenovations.Add(renovation);
            }
        }

        public bool CanExecute_CancelRenovationCommand(object param)
        {
            return SelectedAccommodationRenovation != null;
        }

        public void Execute_CancelRenovationCommand(object param)
        {
            _accommodationRenovationController.CancelRenovation(SelectedAccommodationRenovation);
            Refresh();
        }
    }
}

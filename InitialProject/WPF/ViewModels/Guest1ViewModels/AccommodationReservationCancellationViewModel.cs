using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class AccommodationReservationCancellationViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private readonly AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        public User guest { get; set; }

        public int userId;

        public AccommodationReservationCancellationViewModel(User user)
        {

            guest = user;


            _accommodationReservationController = new AccommodationReservationController();

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetCancelledByUserId(guest.Id));
        }

        private void UpdateAccommodationReservationsList()
        {
            AccommodationReservations.Clear();
            foreach (var accommodationReservation in _accommodationReservationController.GetAll())
            {
                AccommodationReservations.Add(accommodationReservation);
            }
        }

        public void Update()
        {
            UpdateAccommodationReservationsList();
        }


        private void CancelButton_Click()
        {
            this.Close();
        }

        private void CancelReservationButton_Click()
        {
            if (SelectedAccommodationReservation != null)
            {
                if (DateTime.Now > SelectedAccommodationReservation.Start.AddDays(-SelectedAccommodationReservation.Accommodation.CancelationPeriod))
                {
                    MessageBox.Show("Prošao je krajnji rok za otkazivanje ove rezervacije!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (SelectedAccommodationReservation.Accommodation.CancelationPeriod == 0)
                {
                    if (DateTime.Now > SelectedAccommodationReservation.Start.AddDays(-1))
                    {
                        MessageBox.Show("Prošao je krajnji rok za otkazivanje ove rezervacije!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        //_accommodationReservationController.Delete(SelectedAccommodationReservation);
                        SelectedAccommodationReservation.IsCancelled = true;
                        _accommodationReservationController.Update(SelectedAccommodationReservation);
                        MessageBox.Show("Uspešno ste otkazali smeštaj!", "Otkazano!", MessageBoxButton.OK);
                        this.Close();
                    }
                }
                else
                {
                    //_accommodationReservationController.Delete(SelectedAccommodationReservation);
                    SelectedAccommodationReservation.IsCancelled = true;
                    _accommodationReservationController.Update(SelectedAccommodationReservation);
                    MessageBox.Show("Uspešno ste otkazali smeštaj!", "Otkazano!", MessageBoxButton.OK);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Prvo morate izabrati rezervaciju!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }









        private ICommand _cancelReservationCommand;
        public ICommand CancelReservationCommand
        {
            get
            {
                return _cancelReservationCommand ?? (_cancelReservationCommand = new CommandBase(() => CancelReservationButton_Click(), true));
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandBase(() => CancelButton_Click(), true));
            }
        }
    }
}

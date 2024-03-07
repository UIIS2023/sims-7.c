using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest1Windows;
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
    public class AccommodationReservationReschedulingSelectionViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private readonly AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        public User guest { get; set; }

        public int userId;

        public AccommodationReservationReschedulingSelectionViewModel(User user)
        {

            guest = user;


            _accommodationReservationController = new AccommodationReservationController();

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetByUserId(guest.Id));
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

        private void AccommodationReservationReschedulingButton_Click()
        {
            if (SelectedAccommodationReservation != null)
            {
                AccommodationReservationReschedulingWindow accommodationReservationReschedulingWindow = new AccommodationReservationReschedulingWindow(SelectedAccommodationReservation, guest);
                accommodationReservationReschedulingWindow.Show();
            }
            else
            {
                MessageBox.Show("Prvo morate odabrati smeštaj!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click()
        {
            this.Close();
        }


        private ICommand _rescheduleCommand;
        public ICommand RescheduleCommand
        {
            get
            {
                return _rescheduleCommand ?? (_rescheduleCommand = new CommandBase(() => AccommodationReservationReschedulingButton_Click(), true));
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

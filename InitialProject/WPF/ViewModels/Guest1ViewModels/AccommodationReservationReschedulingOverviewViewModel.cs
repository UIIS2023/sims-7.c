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
    public class AccommodationReservationReschedulingOverviewViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private readonly ReservationRescheduleRequestController _reservationRescheduleRequestController;

        public ObservableCollection<ReservationRescheduleRequest> StandByReservationRescheduleRequests { get; set; }
        public ObservableCollection<ReservationRescheduleRequest> ApprovedReservationRescheduleRequests { get; set; }
        public ObservableCollection<ReservationRescheduleRequest> DeclinedReservationRescheduleRequests { get; set; }

        public User guest { get; set; }

        public AccommodationReservationReschedulingOverviewViewModel(User user)
        {
            guest = user;

            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();

            StandByReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestController.GetStandBy(guest.Id));
            ApprovedReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestController.GetApproved(guest.Id));
            DeclinedReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestController.GetDeclined(guest.Id));
        }

        /*
        private void UpdateRequestsList()
        {
            ReservationRescheduleRequests.Clear();
            foreach (var accommodation in _reservationRescheduleRequestController.GetAll())
            {
                ReservationRescheduleRequests.Add(accommodation);
            }
        }

        public void Update()
        {
            UpdateRequestsList();
        }
        */

        private void AccommodationReservationReschedulingSelectionButton_Click()
        {
            AccommodationReservationReschedulingSelectionWindow accommodationReservationReschedulingSelectionWindow = new AccommodationReservationReschedulingSelectionWindow(guest);
            accommodationReservationReschedulingSelectionWindow.Show();
        }

        private void CancelButton_Click()
        {
            this.Close();
        }





        private ICommand _selectionCommand;
        public ICommand SelectionCommand
        {
            get
            {
                return _selectionCommand ?? (_selectionCommand = new CommandBase(() => AccommodationReservationReschedulingSelectionButton_Click(), true));
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

using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestHandlingViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;
        public AccommodationReservationController _accommodationReservationController;

        #region NotifyProperties
        private string _guest;
        public string Guest
        {
            get => _guest;
            set
            {
                if (value != _guest)
                {
                    _guest = value;
                    OnPropertyChanged("Guest");
                }
            }
        }

        private string _available;
        public string Available
        {
            get => _available;
            set
            {
                if (value != _available)
                {
                    _available = value;
                    OnPropertyChanged("Available");
                }
            }
        }
        #endregion

        public ReservationRescheduleRequest rescheduleRequest { get; set; }

        public RelayCommand AcceptCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }
        public ReservationRescheduleRequestHandlingViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();

            rescheduleRequest = reservationRescheduleRequest;
            Guest = rescheduleRequest.Guest.FirstName + " " + rescheduleRequest.Guest.LastName;
            if (!_accommodationReservationController.IsReschedulePossible(rescheduleRequest))
            {
                Available = "Smeštaj je zauzet.";
                MessageBox.Show("Smeštaj je rezervisan u traženim datumima.");

            }
            else
            {
                Available = "Smeštaj je slobodan.";
            }

            AcceptCommand = new RelayCommand(Execute_AcceptCommand, CanAccept);

            CancelCommand = new RelayCommand(Execute_CancelCommand);
        }

        public bool CanAccept(object param)
        {
            return true;
        }
        private void Execute_AcceptCommand(object sender)
        {
            rescheduleRequest.Status = Enumerations.RequestStatusType.Approved;
            rescheduleRequest.Reservation.Start = rescheduleRequest.NewStart;
            rescheduleRequest.Reservation.End = rescheduleRequest.NewEnd;
            _accommodationReservationController.Update(rescheduleRequest.Reservation);
            _reservationRescheduleRequestController.Update(rescheduleRequest);
            Close();
        }

        private void Execute_CancelCommand(object sender)
        {
            DeclineReservationRescheduleRequestCommentWindow Comment = new DeclineReservationRescheduleRequestCommentWindow(rescheduleRequest);
            Comment.Show();
            Close();

        }


    }
}

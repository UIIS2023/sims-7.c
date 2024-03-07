using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class AccommodationReservationReschedulingViewModel : ViewModelBase, IClose
    {
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;
        public AccommodationReservationController _accommodationReservationController;
        public User guest { get; set; }
        public string comment;
        public RequestStatusType status { get; set; }

        public Action Close { get; set; }

        /*
        #region NotifyProperties
        private DateTime _selectedNewStartDate;
        public DateTime SelectedNewStartDate
        {
            get => _selectedNewStartDate;
            set
            {
                if (value != _selectedNewStartDate)
                {
                    _selectedNewStartDate = value;
                    OnPropertyChanged("SelectedNewStartDate");
                }
            }
        }

        private DateTime _selectedNewEndDate;
        public DateTime SelectedNewEndDate
        {
            get => _selectedNewEndDate;
            set
            {
                if (value != _selectedNewEndDate)
                {
                    _selectedNewEndDate = value;
                    OnPropertyChanged("SelectedNewEndDate");
                }
            }
        }

        #endregion
        */

        DateTime SelectedNewStartDate;
        private void NewStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedNewStartDate = (DateTime)e.AddedItems[0];
        }

        DateTime SelectedNewEndDate;
        private void NewEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedNewEndDate = (DateTime)e.AddedItems[0];
        }

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
        public AccommodationReservation reservation { get; set; }

        public AccommodationReservationReschedulingViewModel(AccommodationReservation accommodationReservation, User user)
        {
            guest = user;

            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();
            reservation = accommodationReservation;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendRescheduleRequestButton_Click()
        {
            if (FromDate == null)
            {
                MessageBox.Show("Niste uneli novi pocetni datum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (ToDate == null)
            {
                MessageBox.Show("Niste uneli novi krajnji datum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedNewEndDate < SelectedNewStartDate)
            {
                MessageBox.Show("Krajnji datum ne moze biti pre pocetnog datuma!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                comment = "";
                status = 0;
                ReservationRescheduleRequest reservationRescheduleRequest = new ReservationRescheduleRequest() { Reservation = reservation, Guest = guest, NewStart = FromDate, NewEnd = ToDate, Comment = comment, Status = status };
                _reservationRescheduleRequestController.Save(reservationRescheduleRequest);
                MessageBox.Show("Uspešno ste poslali zahtev za pomeranje rezervacije smeštaja!", "Poslato!", MessageBoxButton.OK);
                this.Close();
            }
        }

        private void CancelButton_Click()
        {
            this.Close();
        }












        private DateTime _fromDate = DateTime.Now;
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
            }
        }

        private DateTime _toDate = DateTime.Now;

        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
            }
        }

        private ICommand _requestRescheduleCommand;
        public ICommand RequestRescheduleCommand
        {
            get
            {
                return _requestRescheduleCommand ?? (_requestRescheduleCommand = new CommandBase(() => SendRescheduleRequestButton_Click(), true));
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

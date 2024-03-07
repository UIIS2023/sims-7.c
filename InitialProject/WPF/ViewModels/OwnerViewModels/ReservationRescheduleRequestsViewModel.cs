using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestsViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationRescheduleRequest> reservationRescheduleRequests;
        public ObservableCollection<ReservationRescheduleRequest> ReservationRescheduleRequests
        {
            get { return reservationRescheduleRequests; }
            set
            {
                reservationRescheduleRequests = value;
                OnPropertyChanged();
            }
        }
        public ReservationRescheduleRequestController _reservationRescheduleRequestsController;

        public ReservationRescheduleRequest SelectedReservationRescheduleRequest { get; set; }

        public RelayCommand RequestHandlingCommand { get; set; }

        public ReservationRescheduleRequestsViewModel()
        {
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllRequestsForHandling());

            RequestHandlingCommand = new RelayCommand(Execute_RequestHandlingCommand, CanHandle);
        }

        public void Refresh()
        {
            ReservationRescheduleRequests.Clear();
            foreach (ReservationRescheduleRequest request in _reservationRescheduleRequestsController.GetAllRequestsForHandling())
            {
                ReservationRescheduleRequests.Add(request);
            }
        }

        public bool CanHandle(object param)
        {
            return SelectedReservationRescheduleRequest != null;
        }
        private void Execute_RequestHandlingCommand(object sender)
        {

            ReservationRescheduleRequestHandlingWindow ReservationRescheduleRequestHandling = new ReservationRescheduleRequestHandlingWindow(SelectedReservationRescheduleRequest);
            ReservationRescheduleRequestHandling.ShowDialog();

            Refresh();

        }

    }
}

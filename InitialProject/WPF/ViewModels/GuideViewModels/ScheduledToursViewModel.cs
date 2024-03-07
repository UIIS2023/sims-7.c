using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.GuideWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuideViewModels
{
    public class ScheduledToursViewModel
    {

        public ObservableCollection<TourEvent> TodaysEvents { get; set; }
        private TourEventController _tourEventController;
        private TourReservationController _tourReservationController;

        public RelayCommand CancelCommand { get; set; }
        public TourEvent SelectedTourEvent { get; set; }
        public ScheduledToursViewModel()
        {
           
            _tourEventController = new TourEventController();
            _tourReservationController = new TourReservationController();

            TodaysEvents = new ObservableCollection<TourEvent>(_tourEventController.GetTourEventsInFuture());
            CancelCommand = new RelayCommand(Executed_CancelCommand, CanExecute_CancelCommand);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return SelectedTourEvent != null;
            
        }

        public void Executed_CancelCommand(object obj)
        {
            _tourReservationController.CancelAllTourReservationsForTourEvent(SelectedTourEvent.Id);
            MessageBox.Show("Uspesno ste otkazali");
        }
        

    }
}

using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Injector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourPointsViewModel
    {

        private NavigationService navigationService;
        public ObservableCollection<TourPoint> TourPoints { get; set; }

        public TourPointController _tourPointController { get; set; }
        public TourEventController _tourEventController { get; set; }


        public TourEvent SelectedTourEvent { get; set; }


        public TourPointsViewModel(NavigationService service, TourEvent selectedTourEvent)
        {
            this.navigationService = service;

            _tourPointController = new TourPointController();
            _tourEventController = new TourEventController();

            SelectedTourEvent = selectedTourEvent;

            _tourPointController = new TourPointController();

            TourPoints = new ObservableCollection<TourPoint>(SelectedTourEvent.Tour.TourPoints);


        }
    }
}

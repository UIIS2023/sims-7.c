using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Navigation;
using System.Threading.Channels;
using InitialProject.WPF.ViewModels;
using InitialProject.Properties;
using System.Resources;

namespace InitialProject.WPF.Views.Guest2Windows
{
    /// <summary>
    /// Interaction logic for MyToursWindow.xaml
    /// </summary>
    public partial class MyToursWindow : INotifyPropertyChanged
    {
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<TourEvent> TourEvents { get; set; }

        public TourEventController _tourEventController;

        public TourReservationController _tourReservationController;

        private TourEvent _selectedTourEvent;
        public TourEvent SelectedTourEvent
        {
            get => _selectedTourEvent;
            set
            {
                if (_selectedTourEvent != value)
                {
                    _selectedTourEvent = value;
                    OnPropertyChanged("SelectedTourEvent");
                }
            }
        }
        public MyToursWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _tourEventController = new TourEventController();
            _tourReservationController = new TourReservationController();

            
            TourEvents = new ObservableCollection<TourEvent>(_tourReservationController.UsersTourEvents(SignInForm.LoggedUser.Id));

        }

        private void TourPoint_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTourEvent == null)
            {
                var resourceManager = new ResourceManager("InitialProject.Properties.Resources", typeof(Resources).Assembly);
                var selectTourMessage = resourceManager.GetString("SelectTour");
                MessageBox.Show(selectTourMessage);
                //MessageBox.Show("Morate selektovati turu!");
                //return;
            }
            else if(_selectedTourEvent.Status != Enumerations.TourEventStatus.Started)
            {
                MessageBox.Show(Properties.Resources.CannotViewKeyPoints);
            }
            else
            {
                TourPointsWindow tourPointsWindow = new TourPointsWindow(SelectedTourEvent);
                tourPointsWindow.Show();
            } 
            
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {

            if (_selectedTourEvent != null && _selectedTourEvent.Status == Enumerations.TourEventStatus.Finished)
            {  
                GuideReviewWindow guideReviewWindow = new GuideReviewWindow(_tourReservationController.GetTourReservationForTourEventAndUser(SelectedTourEvent.Id,SignInForm.LoggedUser.Id));
                guideReviewWindow.Show();
                
            }
            else if (_selectedTourEvent == null)
            {
                MessageBox.Show("Morate odabrati turu!");
            }
            else
            {
                MessageBox.Show("Mozete samo da ocenite ture koje su zavrsene");
            }
            
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            CreateTourRequestWindow createTourRequestWindow = new CreateTourRequestWindow();
            createTourRequestWindow.Show();
        }
    }
}

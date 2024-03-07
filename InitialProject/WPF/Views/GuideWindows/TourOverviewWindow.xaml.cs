using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.GuideWindows
{
    /// <summary>
    /// Interaction logic for TourOverviewWindow.xaml
    /// </summary>
    public partial class TourOverviewWindow : Page
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public TourController _tourController;

        public TourOverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _tourController = new TourController();

            Tours = new ObservableCollection<Tour>(_tourController.GetAllToursForGuide(SignInForm.LoggedUser.Id));
        }

        private void CreateToursButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourWindow createTour = new CreateTourWindow();
            
            Refresh();
            this.NavigationService.Navigate(createTour);

        }

        public void Refresh()
        {
            Tours.Clear();
            foreach (Tour tour in _tourController.GetByGuide(SignInForm.LoggedUser.Id))
            {
                Tours.Add(tour);
            }
        }
        private void UpdateToursList()
        {

            foreach (var tour in _tourController.GetAllToursForGuide(SignInForm.LoggedUser.Id))
            {
                Tours.Add(tour);
            }
        }

        public void Update()
        {
            UpdateToursList();
        }
    }
}

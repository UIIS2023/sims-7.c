using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccommodationOverviewWindow.xaml
    /// </summary>
    public partial class AccommodationOverviewWindow : UserControl
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationController _accommodationController;

        public Accommodation SelectedAccommodation { get; set; }

        public RelayCommand RegisterNewAccommodationCommand { get; set; }
        public RelayCommand ReviewsCommand { get; set; }

        public RelayCommand AccommodationRenovationCommand { get; set; }

        public RelayCommand ShowAccommodationRenovationCommand { get; set; }

        public RelayCommand ShowStatisticByYearCommand { get; set; }

        public AccommodationOverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            RegisterNewAccommodationCommand = new RelayCommand(RegistenNewAccommodationButton_Click, CanRegisterNewAccommodation);
            ReviewsCommand = new RelayCommand(AccommodationReviewsButton_Click, CanReview);
            AccommodationRenovationCommand = new RelayCommand(Execute_AccommodationRenovationCommand, CanExecute_AccommodationRenovationCommand);
            ShowAccommodationRenovationCommand = new RelayCommand(Execute_ShowAccommodationRenovationCommand, CanExecute_ShowAccommodationRenovationCommand);
            ShowStatisticByYearCommand = new RelayCommand(Execute_ShowStatisticByYearCommand, CanExecute_ShowStatisticByYearCommand);

            _accommodationController = new AccommodationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id)); 
            if(Accommodations.Count != 0)
            {
                SelectedAccommodation = Accommodations.ElementAt(0);
            }

          


        }
        void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            foreach (InputBinding ib in this.InputBindings)
            {
                window.InputBindings.Add(ib);
            }
        }

        public void Refresh()
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in _accommodationController.GetByOwner(SignInForm.LoggedUser.Id))
            {
                Accommodations.Add(accommodation);
            }
        }



        private bool CanRegisterNewAccommodation(object param)
        {
            return MainWindow.SelectedTab == 1;
        }

        private void RegistenNewAccommodationButton_Click(object sender)
        {
            RegisterNewAccommodation NewAccommodation = new RegisterNewAccommodation();
            NewAccommodation.ShowDialog();

            Refresh();
            
        }

        private bool CanReview(object param)
        {
            return SelectedAccommodation != null && MainWindow.SelectedTab == 1;
        }

        private void AccommodationReviewsButton_Click(object param)
        {
            
            AccommodationReviewsWindow AccommodationReviews = new AccommodationReviewsWindow(SelectedAccommodation);
            AccommodationReviews.Show();
        }

        private bool CanExecute_AccommodationRenovationCommand(object param)
        {
            return SelectedAccommodation != null && MainWindow.SelectedTab == 1;
        }

    

        private void Execute_AccommodationRenovationCommand(object param)
        {
            ScheduleAccommodationRenovationWindow renovation = new ScheduleAccommodationRenovationWindow(SelectedAccommodation);
            renovation.Show();
        }

        public bool CanExecute_ShowAccommodationRenovationCommand(object param)
        {
            return SelectedAccommodation != null && MainWindow.SelectedTab == 1;
        }

        private void Execute_ShowAccommodationRenovationCommand(object param)
        {
            AccommodationRenovationsOverviewWindow renovation = new AccommodationRenovationsOverviewWindow(SelectedAccommodation);
            renovation.Show();
        }

        public bool CanExecute_ShowStatisticByYearCommand(object param)
        {
            return SelectedAccommodation != null && MainWindow.SelectedTab == 1;
        }

        private void Execute_ShowStatisticByYearCommand(object param)
        {
            AccommodationStatisticsByYearDto statistics = new AccommodationStatisticsByYearDto(SelectedAccommodation);
            statistics.Show();
        }
    }
}

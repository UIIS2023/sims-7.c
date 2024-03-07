using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerWindows;
using System;
using System.Collections.Generic;
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

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        private AccommodationOwnerReviewController _accomodationOwnerReviewController;

        public OwnerMainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _accomodationOwnerReviewController = new AccommodationOwnerReviewController();


            if (!_accomodationOwnerReviewController.IsSuperOwner(SignInForm.LoggedUser.Id))
            {
                SuperOwnerLabel.Visibility = Visibility.Hidden;
            }
            


        }

        private void MyAccommodationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationOverviewWindow AccommodationOverview = new AccommodationOverviewWindow();
            //AccommodationOverview.Show();
        }



        private void GuestsReviewButton_Click(object sender, RoutedEventArgs e)
        {
            GuestWithoutReviewWindow GuestWithoutReview = new GuestWithoutReviewWindow();
            //GuestWithoutReview.Show();

        }

        private void ReservationRescheduleRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationRescheduleRequestsWindow ReservationRescheduleRequests = new ReservationRescheduleRequestsWindow();
            //ReservationRescheduleRequests.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}

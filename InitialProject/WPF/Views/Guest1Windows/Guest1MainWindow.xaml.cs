using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest2Windows;
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

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public User guest { get; set; }

        public Guest1MainWindow(User user)
        {
            InitializeComponent();

            guest = user;
        }

        private void AccommodationsOverview_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsOverview accommodationsOverview = new AccommodationsOverview(guest);
            accommodationsOverview.Show();
        }

        private void AccommodationOwnerReviewSelection_Click(object sender, RoutedEventArgs e)
        {
            AccommodationOwnerReviewSelectionWindow accommodationOwnerReviewSelectionWindow = new AccommodationOwnerReviewSelectionWindow(guest);
            accommodationOwnerReviewSelectionWindow.Show();
        }

        private void GuestReviewsOverview_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsOverviewWindow guestReviewsOverviewWindow = new GuestReviewsOverviewWindow(guest);
            guestReviewsOverviewWindow.Show();
        }

        private void AccommodationReservationReschedulingRequest_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingOverviewWindow accommodationReservationReschedulingOverviewWindow = new AccommodationReservationReschedulingOverviewWindow(guest);
            accommodationReservationReschedulingOverviewWindow.Show();
        }

        private void AccommodationReservationCancellation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationCancellationWindow accommodationReservationCancellationWindow = new AccommodationReservationCancellationWindow(guest);
            accommodationReservationCancellationWindow.Show();
        }

        private void WhereverWhenever_Click(object sender, RoutedEventArgs e)
        {
            WhereverWheneverWindow whereverWheneverWindow = new WhereverWheneverWindow(guest);
            whereverWheneverWindow.Show();
        }

        private void ForumsOverview_Click(object sender, RoutedEventArgs e)
        {
            ForumsOverview forumsOverview = new ForumsOverview(guest);
            forumsOverview.Show();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow(guest);
            profileWindow.Show();
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            TutorialWindow tutorialWindow = new TutorialWindow();
            tutorialWindow.Show();
        }
    }
}

using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.GuideViewModels;
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
    /// Interaction logic for ReviewsInformationWindow.xaml
    /// </summary>
    public partial class ReviewsInformationWindow : Page
    {
        public NavigationService navigationService;
        public ReviewsInformationWindow(GuideReview user)
        {
            InitializeComponent();
            ReviewsInformationViewModel reviewsInformationViewModel = new ReviewsInformationViewModel(navigationService, user);
            this.DataContext = reviewsInformationViewModel;

        }
        private void ReviewsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}

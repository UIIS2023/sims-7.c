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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using InitialProject.WPF.ViewModels.Guest2ViewModels;

namespace InitialProject.WPF.Views.Guest2Windows
{
    /// <summary>
    /// Interaction logic for GuideReviewWindow.xaml
    /// </summary>
 
    public partial class GuideReviewWindow : Window
    {
        public NavigationService navigationService;

        public GuideReviewViewModel guideReviewViewModel;

        public GuideReviewWindow(TourReservation SelectedTourReservation)
        {
            InitializeComponent();
            //navigationService = NavigationService.GetNavigationService(this);
            guideReviewViewModel = new GuideReviewViewModel(navigationService, SelectedTourReservation);
            this.DataContext = guideReviewViewModel;
        }
/*
        private void SetReviewForGuideKnowledge(object sender, RoutedEventArgs e)
        {
            if (knowledge1.IsChecked == true)
                guideReviewViewModel.SelectedKnowledge = 1;
            else if (knowledge2.IsChecked == true)
                guideReviewViewModel.SelectedKnowledge = 2;
            else if (knowledge3.IsChecked == true)
                guideReviewViewModel.SelectedKnowledge = 3;
            else if (knowledge4.IsChecked == true)
                guideReviewViewModel.SelectedKnowledge = 4;
            else if (knowledge5.IsChecked == true)
                guideReviewViewModel.SelectedKnowledge = 5;
        }

        private void SetReviewForGuideLanguage(object sender, RoutedEventArgs e)
        {
            if (language1.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 1;
            else if (language2.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 2;
            else if (language3.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 3;
            else if (language4.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 4;
            else if (language5.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 5;
        }

        private void SetReviewForGuideInterestingness(object sender, RoutedEventArgs e)
        {
            if (interestingness1.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 1;
            else if (interestingness2.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 2;
            else if (interestingness3.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 3;
            else if (interestingness4.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 4;
            else if (interestingness5.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 5;
        }*/
    }
}

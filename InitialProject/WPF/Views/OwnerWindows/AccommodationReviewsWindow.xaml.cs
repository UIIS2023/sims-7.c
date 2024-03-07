using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccommodationReviewsWindow.xaml
    /// </summary>
    public partial class AccommodationReviewsWindow : Window
    {
        public AccommodationReviewsWindow(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.AccommodationReviewsViewModel(accommodation);

            if (DataContext is IClose vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }

        }
      
    }
}

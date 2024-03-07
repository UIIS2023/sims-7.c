using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
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
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest1ViewModels;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerReviewWindow.xaml
    /// </summary>
    public partial class AccommodationOwnerReviewWindow : Window
    {
        public AccommodationOwnerReviewWindow(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            AccommodationOwnerReviewViewModel accommodationOwnerReviewViewModel = new AccommodationOwnerReviewViewModel(accommodationReservation);
            this.DataContext = accommodationOwnerReviewViewModel;

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

using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AccommodationReservationReschedulingWindow.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingWindow : Window
    {
        public AccommodationReservationReschedulingWindow(AccommodationReservation accommodationReservation, User user)
        {
            InitializeComponent();
            AccommodationReservationReschedulingViewModel accommodationReservationReschedulingViewModel = new AccommodationReservationReschedulingViewModel(accommodationReservation, user);
            DataContext = accommodationReservationReschedulingViewModel;

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

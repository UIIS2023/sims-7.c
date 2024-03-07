using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequestHandlingWindow.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestHandlingWindow : Window
    {
        public ReservationRescheduleRequestHandlingWindow(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.ReservationRescheduleRequestHandlingViewModel(reservationRescheduleRequest);
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

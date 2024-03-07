using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for DeclineReservationRescheduleRequestCommentWindow.xaml
    /// </summary>
    public partial class DeclineReservationRescheduleRequestCommentWindow : Window
    {
        public DeclineReservationRescheduleRequestCommentWindow(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.DeclineReservationRescheduleRequestCommentViewModel(reservationRescheduleRequest);
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

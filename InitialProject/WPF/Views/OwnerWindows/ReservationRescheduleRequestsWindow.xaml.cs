using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequestsWindow.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestsWindow : UserControl
    {
        
        public ReservationRescheduleRequestsWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.ReservationRescheduleRequestsViewModel();

        }

    
    }
}

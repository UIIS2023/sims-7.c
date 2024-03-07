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
using System.Windows.Shapes;
using InitialProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Navigation;

namespace InitialProject.WPF.Views.Guest2Windows
{
    /// <summary>
    /// Interaction logic for TourPointsWindow.xaml
    /// </summary>
    public partial class TourPointsWindow : Window
    {
        public NavigationService navigationService;
        public TourPointsWindow(TourEvent SelectedTourEvent)
        {
            InitializeComponent();
            this.DataContext = new TourPointsViewModel(navigationService, SelectedTourEvent);
        }
    }
}

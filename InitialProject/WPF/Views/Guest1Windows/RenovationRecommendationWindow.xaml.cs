using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Properties;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest1ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
using static System.Net.Mime.MediaTypeNames;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for RenovationRecommendationWindow.xaml
    /// </summary>
    public partial class RenovationRecommendationWindow : Window
    {
        public RenovationRecommendationWindow(AccommodationReservation accommodationReservation, List<RenovationRecommendation> saveRenovationRecommendations)
        {
            InitializeComponent();
            RenovationRecommendationViewModel renovationRecommendationViewModel = new RenovationRecommendationViewModel(accommodationReservation, saveRenovationRecommendations);
            this.DataContext = renovationRecommendationViewModel;

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

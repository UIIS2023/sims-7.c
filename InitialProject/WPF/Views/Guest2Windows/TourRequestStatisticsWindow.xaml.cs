using System;
using System.Collections.Generic;
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
using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest2ViewModels;

namespace InitialProject.WPF.Views.Guest2Windows
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsWindow.xaml
    /// </summary>
    public partial class TourRequestStatisticsWindow : Window
    {
        public TourRequestStatisticsWindow()
        {
            InitializeComponent();

            this.DataContext = new TourRequestStatisticsViewModel();
        }

    }
}

using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.WPF.Views.GuideWindows
{
    /// <summary>
    /// Interaction logic for ScheduledTours.xaml
    /// </summary>
    public partial class ScheduledTours : Window
    {

      
        public ScheduledTours()
        {
            InitializeComponent();
            ScheduledToursViewModel scheduledToursViewModel = new ScheduledToursViewModel();
            this.DataContext = scheduledToursViewModel;

        }

       
    }
}

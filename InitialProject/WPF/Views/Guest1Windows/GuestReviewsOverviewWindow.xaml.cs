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
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest1ViewModels;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for GuestReviewsOverviewWindow.xaml
    /// </summary>
    public partial class GuestReviewsOverviewWindow : Window
    {
        public GuestReviewsOverviewWindow(User user)
        {
            InitializeComponent();
            GuestReviewsOverviewViewModel guestReviewsOverviewViewModel = new GuestReviewsOverviewViewModel(user);
            this.DataContext = guestReviewsOverviewViewModel;

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

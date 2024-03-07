using InitialProject.Controller;
using InitialProject.Domain.Models;
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
using System.Windows.Shapes;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest1ViewModels;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerReviewSelectionWindow.xaml
    /// </summary>
    public partial class AccommodationOwnerReviewSelectionWindow : Window
    {
        public AccommodationOwnerReviewSelectionWindow(User user)
        {
            InitializeComponent();
            AccommodationOwnerReviewSelectionViewModel accommodationOwnerReviewSelectionViewModel = new AccommodationOwnerReviewSelectionViewModel(user);
            DataContext = accommodationOwnerReviewSelectionViewModel;

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

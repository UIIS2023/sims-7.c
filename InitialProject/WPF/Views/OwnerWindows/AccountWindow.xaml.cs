using InitialProject.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : UserControl
    {
        private AccommodationOwnerReviewController _accomodationOwnerReviewController;
        public AccountWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _accomodationOwnerReviewController = new AccommodationOwnerReviewController();


            if (!_accomodationOwnerReviewController.IsSuperOwner(SignInForm.LoggedUser.Id))
            {
                SuperOwnerLabel.Visibility = Visibility.Hidden;
                SuperOwnerIcon.Visibility = Visibility.Hidden;
            }
        }
    }
}

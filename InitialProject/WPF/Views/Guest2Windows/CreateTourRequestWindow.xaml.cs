using InitialProject.Controller;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest2ViewModels;
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

namespace InitialProject.WPF.Views.Guest2Windows
{
    /// <summary>
    /// Interaction logic for CreateTourRequest.xaml
    /// </summary>
    public partial class CreateTourRequestWindow : Window
    {
        public CreateTourRequestViewModel createTourRequestViewModel;

        public NavigationService NavigationService { get; set; }

        public CreateTourRequestWindow()
        {
            InitializeComponent();
            createTourRequestViewModel = new CreateTourRequestViewModel(NavigationService);
            this.DataContext = createTourRequestViewModel;

            if (DataContext is IClose vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }

        private void CountryComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            List<string> cities = createTourRequestViewModel._locationController.GetCitiesByCountry(createTourRequestViewModel.SelectedCountry);
            createTourRequestViewModel.Cities.Clear();
            foreach (string city in cities)
            {
                createTourRequestViewModel.Cities.Add(city);
            }
        }

        private void LanguageComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == 0)
            {
                createTourRequestViewModel.Language = "srpski";
            }
            else if (LanguageComboBox.SelectedIndex == 1)
            {

                createTourRequestViewModel.Language = "engleski";

            }
            else if (LanguageComboBox.SelectedIndex == 2)
            {

                createTourRequestViewModel.Language = "italijanski";

            }
            else if (LanguageComboBox.SelectedIndex == 3)
            {
                createTourRequestViewModel.Language = "korejski";
            }
            else if (LanguageComboBox.SelectedIndex == 4)
            {
                createTourRequestViewModel.Language = "japanski";
            }


        }
    }
}

using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels;
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

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccommodationStatisticsByMonth.xaml
    /// </summary>
    public partial class AccommodationStatisticsByMonth : Window
    {
        public AccommodationStatisticsByMonth(AccommodationByYearStatisticDto selectedStatistic, Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.AccommodationStatisticsByMonthViewModel(selectedStatistic, accommodation);

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

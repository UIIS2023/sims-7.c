using InitialProject.Enumerations;
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
    /// Interaction logic for ImageAddingWindow.xaml
    /// </summary>
    public partial class ImageAddingWindow : Window
    {
        public ImageAddingWindow(ImageResource resource, List<Domain.Models.Image> saveImages)
        {
            InitializeComponent();
            ImageAddingViewModel imageAddingViewModel = new ImageAddingViewModel(resource, saveImages);
            this.DataContext = imageAddingViewModel;

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

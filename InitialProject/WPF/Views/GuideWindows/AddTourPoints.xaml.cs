using InitialProject.Domain.Models;
using InitialProject.Enumerations;
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

namespace InitialProject.WPF.Views.GuideWindows
{
    /// <summary>
    /// Interaction logic for AddTourPoints.xaml
    /// </summary>
    public partial class AddTourPoints : Window
    {

        public ObservableCollection<TourPoint> AllTourPoints { get; set; }

        #region NotifyProperties
        private string _name;
        public string Namee
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Namee");
                }
            }
        }

        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;


        
        public List<TourPoint> SaveTourPoints { get; set; }
        
        
        public AddTourPoints( List<TourPoint> saveTourPoints)
        {
           
            InitializeComponent();
            this.DataContext = this;
            

            AllTourPoints = new ObservableCollection<TourPoint>();
            SaveTourPoints = saveTourPoints;
        }

        

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
        private void SubmitButton_Click(object sender, RoutedEventArgs e) {

           
            
            TourPoint tourPoint = new TourPoint(-1, Namee, null, -1 , false);
            AllTourPoints.Add(tourPoint);
            SaveTourPoints.Add(tourPoint);

        }

        
        private void AllTourPointsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

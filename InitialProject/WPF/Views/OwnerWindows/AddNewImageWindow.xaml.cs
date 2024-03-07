using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
//using System.Windows.Controls;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AddNewImageWindow.xaml
    /// </summary>
    public partial class AddNewImageWindow : Window, INotifyPropertyChanged
    {

        public ObservableCollection<Image> AllImages { get; set; }

        #region NotifyProperties
        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }
        }
        private ImageResource _resource;
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
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


        public ImageResource Resource { get; set; }
        public List<Image> SaveImages { get; set; }

        public RelayCommand FinishCommand { get; set; }

        public RelayCommand AddImageCommand { get; set; }
        public AddNewImageWindow(ImageResource resource, List<Image> saveImages)
        {
            InitializeComponent();
            this.DataContext = this;
            Resource = resource;

            AllImages = new ObservableCollection<Image>();
            SaveImages = saveImages;

            FinishCommand = new RelayCommand(CancelButton_Click);
 
            AddImageCommand = new RelayCommand(SubmitButton_Click, CanAddImage);
          
        }

        public bool CanAddImage(object param)
        {
            return true;
        }

        private void SubmitButton_Click(object sender)
        {
            Image image = new Image(-1, Url, -1, Description, Resource);
            AllImages.Add(image);
            SaveImages.Add(image);
        }

        private void CancelButton_Click(object sender)
        {
            Close();
        }

        
    }
}

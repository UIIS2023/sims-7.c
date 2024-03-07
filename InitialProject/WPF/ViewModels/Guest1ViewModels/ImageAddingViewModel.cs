using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class ImageAddingViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        public ObservableCollection<Domain.Models.Image> AllImages { get; set; }

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
        public List<Domain.Models.Image> SaveImages { get; set; }


        public ImageAddingViewModel(ImageResource resource, List<Domain.Models.Image> saveImages)
        {
            Resource = resource;

            AllImages = new ObservableCollection<Domain.Models.Image>();
            SaveImages = saveImages;
        }

        private void AddImageButton_Click()
        {
            if (string.IsNullOrEmpty(Url))
            {
                MessageBox.Show("Niste uneli URL slike!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(Description))
            {
                MessageBox.Show("Niste uneli opis slike!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Domain.Models.Image image = new Domain.Models.Image(-1, Url, -1, Description, Resource);
                AllImages.Add(image);
                SaveImages.Add(image);
            }
        }

        private void CancelButton_Click()
        {
            Close();
        }











        private ICommand _addImageCommand;
        public ICommand AddImageCommand
        {
            get
            {
                return _addImageCommand ?? (_addImageCommand = new CommandBase(() => AddImageButton_Click(), true));
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandBase(() => CancelButton_Click(), true));
            }
        }
    }
}

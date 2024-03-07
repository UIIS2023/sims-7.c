using InitialProject.Domain.Models;
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
    public class RenovationRecommendationViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        public ObservableCollection<int> UrgencyLevels { get; set; }

        public ObservableCollection<RenovationRecommendation> AllRenovationRecommendations { get; set; }

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

        private int _urgencyLevel;
        public int UrgencyLevel
        {
            get => _urgencyLevel;
            set
            {
                if (value != _urgencyLevel)
                {
                    _urgencyLevel = value;
                    OnPropertyChanged("UrgencyLevel");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<RenovationRecommendation> SaveRenovationRecommendations { get; set; }
        public int ResourceId;

        public RenovationRecommendationViewModel(AccommodationReservation accommodationReservation, List<RenovationRecommendation> saveRenovationRecommendations)
        {
            UrgencyLevels = new ObservableCollection<int>();
            UrgencyLevels.Add(1);
            UrgencyLevels.Add(2);
            UrgencyLevels.Add(3);
            UrgencyLevels.Add(4);
            UrgencyLevels.Add(5);

            ResourceId = accommodationReservation.Id;
            AllRenovationRecommendations = new ObservableCollection<RenovationRecommendation>();
            SaveRenovationRecommendations = saveRenovationRecommendations;

        }

        private void Button_Click()
        {
            this.Close();
        }

        private void AddRenovationRecommendation_Click()
        {
            if (string.IsNullOrEmpty(Description))
            {
                MessageBox.Show("Niste uneli opis!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (UrgencyLevel == -1)
            {
                MessageBox.Show("Niste uneli nivo hitnosti!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                RenovationRecommendation renovationRecommendation = new RenovationRecommendation(-1, ResourceId, Description, UrgencyLevel);
                AllRenovationRecommendations.Add(renovationRecommendation);
                SaveRenovationRecommendations.Add(renovationRecommendation);
                this.Close();
            }
        }






        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandBase(() => Button_Click(), true));
            }
        }

        private ICommand _addRecommendationCommand;
        public ICommand AddRecommendationCommand
        {
            get
            {
                return _addRecommendationCommand ?? (_addRecommendationCommand = new CommandBase(() => AddRenovationRecommendation_Click(), true));
            }
        }
    }
}

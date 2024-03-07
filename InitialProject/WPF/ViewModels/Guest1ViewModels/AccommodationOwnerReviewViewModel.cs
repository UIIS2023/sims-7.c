using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using InitialProject.WPF.Views.Guest1Windows;
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
    public class AccommodationOwnerReviewViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        public ObservableCollection<int> Grades { get; set; }
        public AccommodationOwnerReviewController _accommodationOwnerReviewController;
        public AccommodationReservationController _accommodationReservationController;

        public List<Domain.Models.Image> AllImages { get; set; }
        public List<RenovationRecommendation> AllRenovationRecommendations { get; set; }

        #region NotifyProperties
        private int _selectedCleanliness;
        public int SelectedCleanliness
        {
            get => _selectedCleanliness;
            set
            {
                if (value != _selectedCleanliness)
                {
                    _selectedCleanliness = value;
                    OnPropertyChanged("SelectedCleanliness");
                }
            }
        }

        private int _selectedCorrectness;
        public int SelectedCorrectness
        {
            get => _selectedCorrectness;
            set
            {
                if (value != _selectedCorrectness)
                {
                    _selectedCorrectness = value;
                    OnPropertyChanged("SelectedBehaviour");
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
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
        public AccommodationReservation reservation { get; set; }

        public AccommodationOwnerReviewViewModel(AccommodationReservation accommodationReservation)
        {

            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();
            _accommodationReservationController = new AccommodationReservationController();
            reservation = accommodationReservation;
            Grades = new ObservableCollection<int>();
            Grades.Add(1);
            Grades.Add(2);
            Grades.Add(3);
            Grades.Add(4);
            Grades.Add(5);

            AllImages = new List<Domain.Models.Image>();
            AllRenovationRecommendations = new List<RenovationRecommendation>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ReviewButton_Click()
        {
            if (SelectedCleanliness == -1)
            {
                MessageBox.Show("Niste uneli ocenu smestaja!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedCleanliness == -1)
            {
                MessageBox.Show("Niste uneli ocenu vlasnika!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(Comment))
            {
                MessageBox.Show("Niste uneli komentar", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AccommodationOwnerReview accommodationOwnerReview = new AccommodationOwnerReview() { Reservation = reservation, Cleanliness = SelectedCleanliness, Correctness = SelectedCorrectness, Comment = Comment, Images = AllImages, RenovationRecommendations = AllRenovationRecommendations };
                _accommodationOwnerReviewController.SaveReviewImagesRecommendation(accommodationOwnerReview);
                reservation.AccommodationReview = accommodationOwnerReview;
                _accommodationReservationController.Update(reservation);
                MessageBox.Show("Uspešno ste ocenili smeštaj i vlasnika!", "Ocenjeno!", MessageBoxButton.OK);
                this.Close();

                // Get the previous window in the application
                var previousWindow = Application.Current.Windows
                    .OfType<Window>()
                    .Reverse()
                    .Skip(1)
                    .FirstOrDefault();

                // Close the previous window if it exists
                previousWindow?.Close();
            }
        }

        private void CancelButton_Click()
        {
            this.Close();
        }

        private void ImagesButton_Click()
        {
            ImageAddingWindow imageAddingWindow = new ImageAddingWindow(ImageResource.Reservation, AllImages);
            imageAddingWindow.Show();
        }

        private void RecommendationButton_Click()
        {
            if (AllRenovationRecommendations.Count > 0)
            {
                MessageBox.Show("Ne možete poslati više od jedne preporuke za renoviranje!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                RenovationRecommendationWindow renovationRecommendationWindow = new RenovationRecommendationWindow(reservation, AllRenovationRecommendations);
                renovationRecommendationWindow.Show();
            }
        }






        private ICommand _imagesButtonCommand;
        public ICommand ImagesButtonCommand
        {
            get
            {
                return _imagesButtonCommand ?? (_imagesButtonCommand = new CommandBase(() => ImagesButton_Click(), true));
            }
        }

        private ICommand _recommendationButtonCommand;
        public ICommand RecommendationButtonCommand
        {
            get
            {
                return _recommendationButtonCommand ?? (_recommendationButtonCommand = new CommandBase(() => RecommendationButton_Click(), true));
            }
        }

        private ICommand _reviewButtonCommand;
        public ICommand ReviewButtonCommand
        {
            get
            {
                return _reviewButtonCommand ?? (_reviewButtonCommand = new CommandBase(() => ReviewButton_Click(), true));
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelButtonCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandBase(() => CancelButton_Click(), true));
            }
        }
    }
}

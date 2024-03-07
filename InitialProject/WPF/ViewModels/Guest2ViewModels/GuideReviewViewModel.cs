using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Injector;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class GuideReviewViewModel : ViewModelBase
    {
        public NavigationService navigationService { get; set; }
        public ObservableCollection<int> Grades { get; set; }
        public ObservableCollection<string> Images { get; set; }

        public GuideReviewController _guideReviewController;
        public TourReservationController _tourReservationController;
        public ImageController _imageController;

        public TourReservation SelectedTourReservation { get; set; }

        public RelayCommand AddImageCommand { get; set; }
        public RelayCommand RemoveImageCommand { get; set; }
        public RelayCommand AddReviewCommand { get; set; }

        public string SelectedUrl { get; set; }

        private int _selectedKnowledge;
        public int SelectedKnowledge
        {
            get => _selectedKnowledge;
            set
            {
                if (value != _selectedKnowledge)
                {
                    _selectedKnowledge = value;
                    OnPropertyChanged("SelectedKnowledge");
                }
            }
        }

        private int _selectedLanguage;
        public int SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (value != _selectedLanguage)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged("SelectedLanguage");
                }
            }
        }

        private int _selectedInterestingness;
        public int SelectedInterestingness
        {
            get => _selectedInterestingness;
            set
            {
                if (value != _selectedInterestingness)
                {
                    _selectedInterestingness = value;
                    OnPropertyChanged("SelectedInterestingness");
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

        public GuideReviewViewModel(NavigationService service, TourReservation tourReservation)
        {
            this.navigationService = service;
            this.AddImageCommand = new RelayCommand(Executed_AddImageCommand, CanExecute_AddImageCommand);
            this.RemoveImageCommand = new RelayCommand(Executed_RemoveImageCommand, CanExecute_RemoveImageCommand);
            this.AddReviewCommand = new RelayCommand(Executed_AddReviewCommand, CanExecute_AddReviewCommand);

            _guideReviewController = new GuideReviewController();
            _tourReservationController = new TourReservationController();
            _imageController = new ImageController();

            Images = new ObservableCollection<string>();

            Grades = new ObservableCollection<int>();
            Grades.Add(1);
            Grades.Add(2);
            Grades.Add(3);
            Grades.Add(4);
            Grades.Add(5);

            SelectedTourReservation = tourReservation;
            SelectedKnowledge = 0;
            SelectedLanguage = 0;
            SelectedInterestingness = 0;
            //Comment = ""
        }

        public void Executed_AddImageCommand(object obj)
        {
            Images.Add(Url);
            Url = "";
        }

        public bool CanExecute_AddImageCommand(object obj)
        {
            return Url!=null;
        }

        public void Executed_RemoveImageCommand(object obj)
        {
            if (SelectedUrl == null)
            {
                return;
            }

            Images.Remove(SelectedUrl);
            SelectedUrl = null;
        }

        public bool CanExecute_RemoveImageCommand(object obj)
        {
            return SelectedUrl!=null;
        }

        public void Executed_AddReviewCommand(object obj)
        {
            List<string> images = new List<string>(Images);
            GuideReview guideReview = new GuideReview(-1, SelectedTourReservation, SelectedKnowledge, SelectedLanguage, SelectedInterestingness, Comment, images, false);

            List<GuideReview> userReviews = _guideReviewController.GetReviewsForUser(SelectedTourReservation.Id, SignInForm.LoggedUser.Id);

            foreach (GuideReview userReview in userReviews)
            {
                if (guideReview != userReview)
                {
                    MessageBox.Show("Vec ste ocenili ovu turu.");
                    return;
                }
            }


            if (IsValid)
            {
                _guideReviewController.Save(guideReview);
                MessageBox.Show("Uspešno ste ocenili!");
                //this.Close();
            }
            else
            {
                MessageBox.Show("Neispravno uneti podaci!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /*this.navigationService.Navigate(
            new Uri("/WPF/Views/Guest2Windows/MyToursWindow.xaml", UriKind.Relative));*/
            //NavigationCommands.BrowseBack.Execute;
        }

        public bool CanExecute_AddReviewCommand(object obj)
        {
            return true;
        }

 

        /*
        private void SetReviewForGuideKnowledge(object sender, RoutedEventArgs e)
        {
            if (knowledge1.IsChecked == true)
                SelectedKnowledge = 1;
            else if (knowledge2.IsChecked == true)
                SelectedKnowledge = 2;
            else if (knowledge3.IsChecked == true)
                SelectedKnowledge = 3;
            else if (knowledge4.IsChecked == true)
                SelectedKnowledge = 4;
            else if (knowledge5.IsChecked == true)
                SelectedKnowledge = 5;
        }

        private void SetReviewForGuideLanguage(object sender, RoutedEventArgs e)
        {
            if (language1.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 1;
            else if (language2.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 2;
            else if (language3.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 3;
            else if (language4.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 4;
            else if (language5.IsChecked == true)
                guideReviewViewModel.SelectedLanguage = 5;
        }

        private void SetReviewForGuideInterestingness(object sender, RoutedEventArgs e)
        {
            if (interestingness1.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 1;
            else if (interestingness2.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 2;
            else if (interestingness3.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 3;
            else if (interestingness4.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 4;
            else if (interestingness5.IsChecked == true)
                guideReviewViewModel.SelectedInterestingness = 5;

        */
        //Validacija

        public string Error => null;

        public string this[string columnName]
        {
            get
            {

                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Ovo polje ne sme biti prazno";
                }


                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Comment" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        
    }
}

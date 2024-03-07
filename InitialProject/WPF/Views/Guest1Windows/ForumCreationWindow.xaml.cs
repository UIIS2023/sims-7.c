using InitialProject.Controller;
using InitialProject.Domain.Models;
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
    /// Interaction logic for ForumCreationWindow.xaml
    /// </summary>
    public partial class ForumCreationWindow : Window
    {
        /*
        public ForumCreationWindow(User user)
        {
            InitializeComponent();
            ForumCreationViewModel forumCreationViewModel = new ForumCreationViewModel(user);
            DataContext = forumCreationViewModel;

            if (DataContext is IClose vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
        */


        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public LocationController _locationController;

        public ForumController _forumController;

        public CommentController _commentController;

        public NewForumNotificationController _notificationController; //
        public List<Comment> AllComments { get; set; }

        public User guest { get; set; }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }
        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (value != _selectedCity)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
        }

        private string _firstComment;
        public string FirstComment
        {
            get => _firstComment;
            set
            {
                if (value != _firstComment)
                {
                    _firstComment = value;
                    OnPropertyChanged("FirstComment");
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

        public event PropertyChangedEventHandler PropertyChanged;

        public ForumCreationWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            guest = user;

            _locationController = new LocationController();
            _forumController = new ForumController();
            _commentController = new CommentController();
            _notificationController = new NewForumNotificationController(); //

            Countries = new ObservableCollection<string>(_locationController.GetAllCountries());
            Cities = new ObservableCollection<string>();

            AllComments = new List<Comment>();
        }

        private void CountryComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            List<string> cities = _locationController.GetCitiesByCountry(SelectedCountry);
            Cities.Clear();
            foreach (string city in cities)
            {
                Cities.Add(city);
            }
        }

        private void CreateForumButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCountry == null)
            {
                MessageBox.Show("Niste uneli državu!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedCity == null)
            {
                MessageBox.Show("Niste uneli grad!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(FirstComment))
            {
                MessageBox.Show("Niste uneli prvi komentar!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Location location = _locationController.FindLocationByCountryCity(SelectedCountry, SelectedCity);
                Forum forum = new Forum() { Location = location, Author = guest, IsOpen = true, Comments = AllComments };
                Comment comment = new Comment() { Text = FirstComment, Author = guest, Role = guest.Type, ForumId = forum.Id, ReportsNumber = 0 };
                AllComments.Add(comment);
                NewForumNotification notification = new NewForumNotification() { Forum = forum, IsDelivered = false }; //

                if (_forumController.AvailableForum(forum))
                {
                    _forumController.SaveForumComment(forum);
                    _notificationController.Save(notification); //
                    MessageBox.Show("Uspešno ste otvorili forum!", "Forum otvoren!", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Forum o ovoj lokaciji već postoji!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

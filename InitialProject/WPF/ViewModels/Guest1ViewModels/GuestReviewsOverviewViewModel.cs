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
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class GuestReviewsOverviewViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        public ObservableCollection<GuestReview> GuestReviews { get; set; }
        public GuestReviewController _guestReviewController;

        public event PropertyChangedEventHandler? PropertyChanged;

        public User guest { get; set; }

        public GuestReviewsOverviewViewModel(User user)
        {
            guest = user;

            _guestReviewController = new GuestReviewController();
            GuestReviews = new ObservableCollection<GuestReview>(_guestReviewController.GetByUserId(guest.Id));
        }

        private void UpdateGuestReviewsList()
        {
            GuestReviews.Clear();
            foreach (var guestReview in _guestReviewController.GetAll())
            {
                GuestReviews.Add(guestReview);
            }
        }

        public void Update()
        {
            UpdateGuestReviewsList();
        }

        private void Button_Click()
        {
            this.Close();
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandBase(() => Button_Click(), true));
            }
        }
    }
}

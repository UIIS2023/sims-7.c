using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest1Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class AccommodationOwnerReviewSelectionViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private readonly AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        public User guest { get; set; }

        public int userId;

        public AccommodationOwnerReviewSelectionViewModel(User user)
        {
            guest = user;


            _accommodationReservationController = new AccommodationReservationController();

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservationsWithoutAccommodationOwnerReview(guest.Id));
        }

        private void UpdateAccommodationReservationsList()
        {
            AccommodationReservations.Clear();
            foreach (var accommodationReservation in _accommodationReservationController.GetAll())
            {
                AccommodationReservations.Add(accommodationReservation);
            }
        }

        public void Update()
        {
            UpdateAccommodationReservationsList();
        }


        private void Button_Click()
        {
            this.Close();
        }

        private void Review_Click()
        {
            if (SelectedAccommodationReservation != null)
            {
                if (DateTime.Now > SelectedAccommodationReservation.End.AddDays(5))
                {
                    MessageBox.Show("Prošao je poslednji rok za ocenjivanje ovog smeštaja!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AccommodationOwnerReviewWindow accommodationOwnerReviewWindow = new AccommodationOwnerReviewWindow(SelectedAccommodationReservation);
                    accommodationOwnerReviewWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Prvo morate odabrati rezervaciju!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private ICommand _reviewCommand;
        public ICommand ReviewCommand
        {
            get
            {
                return _reviewCommand ?? (_reviewCommand = new CommandBase(() => Review_Click(), true));
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
    }
}

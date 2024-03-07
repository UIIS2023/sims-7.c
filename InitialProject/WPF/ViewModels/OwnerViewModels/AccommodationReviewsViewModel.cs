using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationReviewsViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }
        public ObservableCollection<AccommodationOwnerReview> AccommodationReviews { get; set; }
        public AccommodationOwnerReviewController _accommodationOwnerReviewController;
        public RelayCommand CloseCommand { get; set; }

        public AccommodationReviewsViewModel(Accommodation accommodation)
        {
            CloseCommand = new RelayCommand(Execute_CloseCommand);

            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();

            AccommodationReviews = new ObservableCollection<AccommodationOwnerReview>(_accommodationOwnerReviewController.GetAllValidReviews(accommodation));
        }

        #region Actions
        private void Execute_CloseCommand(object sender)
        {
            this.Close();
        }
        #endregion





    }
}

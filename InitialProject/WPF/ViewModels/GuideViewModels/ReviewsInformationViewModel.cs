using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using InitialProject.WPF.Views.GuideWindows;
using InitialProject.Commands;

namespace InitialProject.WPF.ViewModels.GuideViewModels
{
    public class ReviewsInformationViewModel : ViewModelBase    
    {
        public NavigationService navigationService { get; set; }

        public GuideReviewController _guideReviewController;
        public GuideReview CurrentUser { get; set; }
        public ObservableCollection<ReviewDto> Reviews { get; set; }
        public ObservableCollection<GuideReview> GuideReviews { get; set; }

        public ReviewDto SelectedReview { get; set; }
        public RelayCommand IsValidCommand { get; set; }
        public ReviewsInformationViewModel(NavigationService service, GuideReview user)
        {
            this.navigationService = service;
            CurrentUser = user;
            _guideReviewController = new GuideReviewController();
            GuideReviews = new ObservableCollection<GuideReview>(_guideReviewController.GetAllGuideReviews(SignInForm.LoggedUser.Id, CurrentUser.Reservation.Guest.Id));
            IsValidCommand = new RelayCommand(Executed_IsValidCommand, CanExecute_IsValidCommand);

            Reviews = new ObservableCollection<ReviewDto>();
            foreach (GuideReview guideReview in GuideReviews)
            {
                ReviewDto guideReviewDto = new ReviewDto(guideReview);
                Reviews.Add(guideReviewDto);
            }
        }

        public bool CanExecute_IsValidCommand(object obj)
        {
            return SelectedReview != null && SelectedReview.Validity == false;
        }

        public void Executed_IsValidCommand(object obj)
        {
            SelectedReview.Validity = true;
            GuideReview guideReview = _guideReviewController.Get(SelectedReview.Id);
            guideReview.Validity = true;
            _guideReviewController.Update(guideReview);
            MessageBox.Show("Uspesno ste prijavili recenziju!");
        }

        
        

    }
}

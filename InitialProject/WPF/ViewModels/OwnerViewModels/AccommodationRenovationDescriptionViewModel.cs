using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationRenovationDescriptionViewModel: ViewModelBase, IClose
    {
        public Action Close { get; set; }
        public AccommodationRenovationController _accommodationRenovationController;

        #region NotifyProperties
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public Accommodation Accommodation { get; set; }
        public AvailableTermsDto AvailableTerms { get; set; }

        public RelayCommand FinishShedulingCommand { get; set; }
        public AccommodationRenovationDescriptionViewModel(Accommodation accommodation, AvailableTermsDto availableTerms)
        {
            _accommodationRenovationController = new AccommodationRenovationController();
            Accommodation = accommodation;
            AvailableTerms = availableTerms;

            FinishShedulingCommand = new RelayCommand(Execute_FinishShedulingCommand, CanExecute_FinishShedulingCommand);
        }

        public bool CanExecute_FinishShedulingCommand(object param)
        {
            return true;
        }

        public void Execute_FinishShedulingCommand(object param)
        {
            AccommodationRenovation accommodationRenovation = new AccommodationRenovation() { Accommodation = Accommodation, Start = AvailableTerms.Start, End = AvailableTerms.End, Description = Description, IsCancelled = false };
            _accommodationRenovationController.Save(accommodationRenovation);
            Close();
        }
    }
}

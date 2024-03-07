using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class DeclineReservationRescheduleRequestCommentViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;

        #region NotifyProperties
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        public ReservationRescheduleRequest ReservationRescheduleRequest { get; set; }

        public RelayCommand FinishCommand { get; set; }
        public DeclineReservationRescheduleRequestCommentViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequest = reservationRescheduleRequest;

            FinishCommand = new RelayCommand(Execute_FinishCommand, CanFinish);

        }

        public bool CanFinish(object param)
        {
            return true;
        }

        private void Execute_FinishCommand(object sender)
        {

            ReservationRescheduleRequest.Status = Enumerations.RequestStatusType.Declined;
            ReservationRescheduleRequest.Comment = Comment;
            _reservationRescheduleRequestController.Update(ReservationRescheduleRequest);
            Close();
        }
    }
}

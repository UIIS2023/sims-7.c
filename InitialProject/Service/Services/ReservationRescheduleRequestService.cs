using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class ReservationRescheduleRequestService
    {
        private IReservationRescheduleRequestRepository _reservationRescheduleRequestRepository;
        public ReservationRescheduleRequestService()
        {
            _reservationRescheduleRequestRepository = Injector.Injector.CreateInstance<IReservationRescheduleRequestRepository>();

        }

        public List<ReservationRescheduleRequest> GetAll()
        {
            return _reservationRescheduleRequestRepository.GetAll();
        }

        public ReservationRescheduleRequest Get(int id)
        {
            return _reservationRescheduleRequestRepository.Get(id);
        }

        public ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest)
        {

            return _reservationRescheduleRequestRepository.Save(reservationRescheduleRequest);
        }

        public void Delete(ReservationRescheduleRequest reservationRescheduleRequest)
        {

            _reservationRescheduleRequestRepository.Delete(reservationRescheduleRequest);

        }

        public ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            return _reservationRescheduleRequestRepository.Update(reservationRescheduleRequest);
        }


        private bool IsRequestOnStandby(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            return reservationRescheduleRequest.Status == Enumerations.RequestStatusType.Standby;
        }

        public List<ReservationRescheduleRequest> GetAllRequestsForHandling()
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests= new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (IsRequestOnStandby(reservationRescheduleRequest) && SignInForm.LoggedUser.Id == reservationRescheduleRequest.Reservation.Accommodation.Owner.Id)
                {
                    reservationRescheduleRequests.Add(reservationRescheduleRequest);
                }
            }

            return reservationRescheduleRequests;
        }

        public List<ReservationRescheduleRequest> GetStandBy(int guest)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (reservationRescheduleRequest.Guest.Id == guest)
                {
                    if (reservationRescheduleRequest.Status == RequestStatusType.Standby)
                    {
                        reservationRescheduleRequests.Add(reservationRescheduleRequest);
                    }
                }
            }

            return reservationRescheduleRequests;
        }

        public List<ReservationRescheduleRequest> GetApproved(int guest)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (reservationRescheduleRequest.Guest.Id == guest)
                {
                    if (reservationRescheduleRequest.Status == RequestStatusType.Approved)
                    {
                        reservationRescheduleRequests.Add(reservationRescheduleRequest);
                    }
                }
            }

            return reservationRescheduleRequests;
        }

        public List<ReservationRescheduleRequest> GetDeclined(int guest)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest ReservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (ReservationRescheduleRequest.Guest.Id == guest)
                {
                    if (ReservationRescheduleRequest.Status == RequestStatusType.Declined)
                    {
                        reservationRescheduleRequests.Add(ReservationRescheduleRequest);
                    }
                }
            }

            return reservationRescheduleRequests;
        }

        public bool IsReservationRescheduled(AccommodationReservation reservation)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = _reservationRescheduleRequestRepository.GetAll();
            foreach (ReservationRescheduleRequest ReservationRescheduleRequest in reservationRescheduleRequests)
            {
                if(ReservationRescheduleRequest.Reservation.Id == reservation.Id && ReservationRescheduleRequest.Status == RequestStatusType.Approved)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

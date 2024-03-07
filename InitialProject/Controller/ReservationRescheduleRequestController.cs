using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class ReservationRescheduleRequestController
    {
        private readonly ReservationRescheduleRequestService _reservationRescheduleRequestService;

        public ReservationRescheduleRequestController()
        {
            _reservationRescheduleRequestService = new ReservationRescheduleRequestService();
        }

        public List<ReservationRescheduleRequest> GetAll()
        {
            return _reservationRescheduleRequestService.GetAll();
        }


        public ReservationRescheduleRequest Get(int id)
        {
            return _reservationRescheduleRequestService.Get(id);
        }

        public ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest)
        {

            return _reservationRescheduleRequestService.Save(reservationRescheduleRequest);
        }

        public void Delete(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestService.Delete(reservationRescheduleRequest);
        }

        public ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            return _reservationRescheduleRequestService.Update(reservationRescheduleRequest);
        }

        public List<ReservationRescheduleRequest> GetAllRequestsForHandling()
        {
            return _reservationRescheduleRequestService.GetAllRequestsForHandling();
        }

        public List<ReservationRescheduleRequest> GetStandBy(int guest)
        {
            return _reservationRescheduleRequestService.GetStandBy(guest);
        }

        public List<ReservationRescheduleRequest> GetApproved(int guest)
        {
            return _reservationRescheduleRequestService.GetApproved(guest);
        }

        public List<ReservationRescheduleRequest> GetDeclined(int guest)
        {
            return _reservationRescheduleRequestService.GetDeclined(guest);
        }
    }
}

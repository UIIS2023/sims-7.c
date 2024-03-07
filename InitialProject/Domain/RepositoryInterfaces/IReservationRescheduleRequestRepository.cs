using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IReservationRescheduleRequestRepository
    {
        List<ReservationRescheduleRequest> GetAll();
        ReservationRescheduleRequest Get(int id);
        ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest);
        void Delete(ReservationRescheduleRequest reservationRescheduleRequest);
        ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest);
    }
}

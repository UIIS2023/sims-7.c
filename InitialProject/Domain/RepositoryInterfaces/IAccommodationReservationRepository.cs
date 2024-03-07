using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        AccommodationReservation Get(int id);
        AccommodationReservation Save(AccommodationReservation accommodationReservation);
        void Delete(AccommodationReservation accommodationReservation);
        AccommodationReservation Update(AccommodationReservation accommodationReservation);
   
    }
}

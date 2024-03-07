using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IGuestReviewRepository
    {
        List<GuestReview> GetAll();
        GuestReview Get(int id);
        GuestReview Save(GuestReview guestReview);
        void Delete(GuestReview guestReview);
        GuestReview Update(GuestReview guestReview);
    }
}

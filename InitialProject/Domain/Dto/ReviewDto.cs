using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string NameOfTourPoint { get; set; }
        public int Interestingness { get; set; }
        public int Knowledge { get; set; }
        public int Language { get; set; }
        public string Comment { get; set; }

        public bool Validity { get; set; }  

        

        public ReviewDto(GuideReview guideReview)
        {
            Id = guideReview.Id;    
            Name = guideReview.Reservation.TourEvent.Tour.Name;
            NameOfTourPoint = guideReview.Reservation.TourPointWhenGuestCame.Name;
            Interestingness = guideReview.Interestingness;
            Knowledge = guideReview.Knowledge;
            Language = guideReview.Language;
            Comment = guideReview.Comment;
            Validity = guideReview.Validity;
        }

    }
}

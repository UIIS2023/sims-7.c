using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Injector
{

    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
        };

        public static void BindComponents()
        {

            VoucherRepository voucherRepository = new VoucherRepository();
            AccommodationOwnerReviewRepository accommodationOwnerReviewRepository = new AccommodationOwnerReviewRepository();
            ReservationRescheduleRequestRepository reservationRescheduleRequestRepository = new ReservationRescheduleRequestRepository();
            NotificationRepository notificationRepository = new NotificationRepository();
            TourRequestRepository tourRequestRepository = new TourRequestRepository();
            TourRequestAcceptedNotificationRepository tourRequestAcceptedNotificationRepository = new TourRequestAcceptedNotificationRepository();
            AccommodationRenovationRepository accommodationRenovationRepository = new AccommodationRenovationRepository();
            NewTourNotificationRepository newTorNotificationRepository = new NewTourNotificationRepository();
            RenovationRecommendationRepository renovationRecommendationRepository = new RenovationRecommendationRepository();
            ForumRepository forumRepository = new ForumRepository();
            CommentRepository commentRepository = new CommentRepository();
            NewForumNotificationRepository newForumNotificationRepository = new NewForumNotificationRepository();
            ComplexTourRequestRepository complexTourRequestRepository = new ComplexTourRequestRepository();

            /*NotificationForRequestRepository notificationForRequestRepository = new NotificationForRequestRepository();
            notificationForRequestRepository.BindNotificationTourRequest();*/




            _implementations.Add(typeof(IVoucherRepository), voucherRepository);
            _implementations.Add(typeof(IAccommodationOwnerReviewRepository), accommodationOwnerReviewRepository);
            _implementations.Add(typeof(IReservationRescheduleRequestRepository), reservationRescheduleRequestRepository);
            _implementations.Add(typeof(INotificationRepository), notificationRepository);
            _implementations.Add(typeof(ITourRequestRepository), tourRequestRepository);
            _implementations.Add(typeof(INewTourNotificationRepository), newTorNotificationRepository);
            _implementations.Add(typeof(ITourRequestAcceptedNotificationRepository), tourRequestAcceptedNotificationRepository);
            _implementations.Add(typeof(IAccommodationRenovationRepository), accommodationRenovationRepository);
            _implementations.Add(typeof(IRenovationRecommendationRepository), renovationRecommendationRepository);
            _implementations.Add(typeof(IForumRepository), forumRepository);
            _implementations.Add(typeof(ICommentRepository), commentRepository);
            _implementations.Add(typeof(INewForumNotificationRepository), newForumNotificationRepository);
            _implementations.Add(typeof(IComplexTourRequestRepository), complexTourRequestRepository);

            notificationRepository.BindNotificationTourReservation();
            reservationRescheduleRequestRepository.BindReservationRescheduleRequestWithAccommodationReservation();
            reservationRescheduleRequestRepository.BindReservationRescheduleRequestWithUser();
            accommodationOwnerReviewRepository.BindAccommodationOwnerReviewWithAccommodationReservation();
            voucherRepository.BindVoucherUser();
            tourRequestRepository.BindTourRequestUser();
            tourRequestRepository.BindTourRequestLocation();
            tourRequestAcceptedNotificationRepository.BindTourRequestAcceptedNotificationTourRequest();
            accommodationRenovationRepository.BindAccommodationRenovationWithAccommodation();
            forumRepository.BindForumLocation();
            newForumNotificationRepository.BindNotificationForum();
            complexTourRequestRepository.BindComplexTourRequestUser();
            //_implementations.Add(typeof(INotificationForRequestRepository), notificationForRequestRepository);





        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}

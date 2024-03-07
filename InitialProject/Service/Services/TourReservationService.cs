using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
﻿using InitialProject.Domain.Dto;

using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Service.Services
{
    internal class TourReservationService
    {
        private TourReservationRepository _tourReservationRepository;
        private IVoucherRepository _voucherRepository;
        private VoucherService _voucherService;

        public TourReservationService()
        {
            _tourReservationRepository = TourReservationRepository.GetInstance();
            _voucherRepository = Injector.Injector.CreateInstance<IVoucherRepository>();
         //   _voucherService = Injector.Injector.CreateInstance<VoucherService>();
            _voucherService = new VoucherService();
        }


        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }

        public TourReservation Get(int id)
        {
            return _tourReservationRepository.Get(id);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            if (tourReservation.Voucher == null)
            {
                tourReservation.Voucher = new Voucher() { Id = -1 };
            }

            if (tourReservation.Voucher.Id != -1)
            {
                tourReservation.Voucher.Used = true;
                _voucherService.Update(tourReservation.Voucher);
            }

            return _tourReservationRepository.Save(tourReservation);
        }


        public void Delete(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }

        public List<User> FindGuestsThatDidntComeYet(TourEvent tourEvent)
        {
            List<User> users = new List<User>();

            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.TourPointWhenGuestCame.Id == -1 && tourReservation.TourEvent.Id == tourEvent.Id)
                {
                    users.Add(tourReservation.Guest);
                }

            }

            return users;
        }

        public TourReservation FindTourReservationForUserAndTourEvent(User user, TourEvent tourEvent)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Guest.Id == user.Id && tourReservation.TourEvent.Id == tourEvent.Id)
                {
                    return tourReservation;

                }
            }
            return null;
        }


        public List<TourEvent> UsersTourEvents(int userId)
        {
            List<TourEvent> tourEvents = new List<TourEvent>();
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Guest.Id == userId)
                {
                    tourEvents.Add(tourReservation.TourEvent);
                }
            }
            return tourEvents;
        }

        public TourReservation GetTourReservationForTourEventAndUser(int tourEventId, int userId)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Guest.Id == userId && tourReservation.TourEvent.Id == tourEventId)
                {
                    return tourReservation;
                }
            }
            return null;
        }

        public void CancelTourReservation(TourReservation tourReservation)
        {
            String name = new String("bskdd");
            Voucher voucher = new Voucher(-1, name, tourReservation.Guest, false, 365, DateTime.Now.AddDays(365));
            _voucherRepository.Save(voucher);

            _tourReservationRepository.Delete(tourReservation);
            
        }

        /*public void CancelAllTourReservationsForTourEvent(int tourEventId)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.TourEvent.Id == tourEventId)
                {
                    CancelTourReservation(tourReservation);
                    MessageBox.Show("Uspesno2");

                }
            }
            
        }*/

        public void CancelAllTourReservationsForTourEvent(int tourEventId)
        {
           
            var allTourReservations = _tourReservationRepository.GetAll();
            for (int i = 0; i < allTourReservations.Count(); i++)
            {
                var tourReservation = allTourReservations.ElementAt(i);
                if (tourReservation.TourEvent.Id == tourEventId)
                {
                    CancelTourReservation(tourReservation);
                }
            }
           
        }

        public List<TourReservation> GetAllTourReservationsForTourEventWherePeopleShowed(int tourEventId)
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.TourEvent.Id == tourEventId && tourReservation.TourPointWhenGuestCame.Id != -1)
                {
                    tourReservations.Add(tourReservation);

                }
            }
            return tourReservations;
        }

        public TourAgeGroupStatistic GetAgeStatisticsForTourEvent(int eventId)
        {
            TourAgeGroupStatistic tourAgeGroupStatistic = new TourAgeGroupStatistic(0, 0, 0);
            foreach (TourReservation tourReservation in GetAllTourReservationsForTourEventWherePeopleShowed(eventId))
            {
                if (tourReservation.Guest.Age <= 18)
                {
                    tourAgeGroupStatistic.To18 += 1;
                }
                else if (tourReservation.Guest.Age > 18 && tourReservation.Guest.Age <= 50)
                {
                    tourAgeGroupStatistic.From18To50 += 1;
                }
                else
                {
                    tourAgeGroupStatistic.From50 += 1;
                }
            }
            return tourAgeGroupStatistic;
        }

        public TourPercentageOfGuestsVouchers GetPercentageOfGuestsWithVouchersForTourEvent(int eventId)
        {

            int guestsWithVoucher = 0;
            int guestWithoutVoucher = 0;

            TourPercentageOfGuestsVouchers tourPercentageOfGuestsVouchers = new TourPercentageOfGuestsVouchers(0, 0);

            foreach (TourReservation tourReservation in GetAllTourReservationsForTourEventWherePeopleShowed(eventId))
            {
                if (tourReservation.Voucher.Id != -1)
                {

                    guestsWithVoucher += 1;
                }
                else
                {
                    guestWithoutVoucher += 1;
                }
            }
            double guestsWithVoucherPercentage = (guestsWithVoucher * 100.0) / (guestsWithVoucher + guestWithoutVoucher);
            double guestsWithoutVoucherPercentage = (guestWithoutVoucher * 100.0) / (guestsWithVoucher + guestWithoutVoucher);
            tourPercentageOfGuestsVouchers.PercentageWithVouchers = (int)Math.Round(guestsWithVoucherPercentage);
            tourPercentageOfGuestsVouchers.PercentageWithoutVouchers = (int)Math.Round(guestsWithoutVoucherPercentage);

            return tourPercentageOfGuestsVouchers;
        }

        

    }
}

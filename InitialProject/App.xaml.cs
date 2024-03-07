using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Localization;
using InitialProject.Repositories;
using InitialProject.WPF.ViewModels;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Color backgroundColor;

        public static Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        

        public void ChangeLanguage(string currLang)
        {
            if (currLang.Equals("en-US"))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-LATN");
            }
        }

        public App()
        {
            AccommodationRepository.GetInstance();
            LocationRepository.GetInstance();
            TourRepository.GetInstance();
            TourEventRepository.GetInstance();
            TourPointRepository.GetInstance();
            ImageRepository.GetInstance();
            TourReservationRepository.GetInstance();



            AccommodationReservationRepository.GetInstance();
            GuestReviewRepository.GetInstance();

            //AccommodationOwnerReviewRepository.GetInstance();

            GuideReviewRepository.GetInstance();
            //UserRepository.GetInstance();


            AccommodationRepository.GetInstance().BindAccomodationLocation();
            AccommodationReservationRepository.GetInstance().BindAccomodationReservationAccommodation();
            AccommodationReservationRepository.GetInstance().BindAccomodationReservationGuest();

            TourRepository.GetInstance().BindTourLocation();
            TourRepository.GetInstance().BindTourGuide();
            TourPointRepository.GetInstance().BindTourPointTour();
            TourEventRepository.GetInstance().BindTourEventTour();
            ImageRepository.GetInstance().BindImageResource();


            TourReservationRepository.GetInstance().BindTourReservationTourPoint();
            TourReservationRepository.GetInstance().BindTourReservationTourEvent();
            TourReservationRepository.GetInstance().BindTourReservationUser();


            GuestReviewRepository.GetInstance().BindGuestReviewWithAccommodationReservation();

            

            GuideReviewRepository.GetInstance().BindGuideReviewWithTourReservation();



            Injector.Injector.BindComponents();

            TourReservationRepository.GetInstance().BindTourReservationVoucher();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set the initial background color
            BackgroundColor = (Color)ColorConverter.ConvertFromString("#DFFDFF");

            // Create the SolidColorBrush for the initial background color
            var appBackgroundBrush = new SolidColorBrush(BackgroundColor);

            // Assign the SolidColorBrush to the resource
            Current.Resources["AppBackgroundBrush"] = appBackgroundBrush;
        }

        
    }
}

using InitialProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest2ViewModels;
using InitialProject.WPF.Views.Guest2Windows;
using System.Reflection.Metadata;
using System.Printing;
using System.IO;
using System.Xml.Linq;
using System.Windows.Xps.Packaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reflection;
using System.Drawing;

namespace InitialProject.WPF.Views.Guest2Window
{
    /// <summary>
    /// Interaction logic for TourReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window, INotifyPropertyChanged
    {

        public TourReservationController _tourReservationController;
        public TourEventController _tourEventController;
        public VoucherController _voucherController;

        private string _availableSpotsText { get; set; }
        private int _availableSpots { get; set; }


        private TourEvent _selectedTourEvent;

        public string AvailableSpotsText
        {
            get => _availableSpotsText;
            set
            {
                if (_availableSpotsText != value)
                {
                    _availableSpotsText = value;
                    OnPropertyChanged("AvailableSpotsText");
                }
            }
        }

        public int AvailableSpots
        {
            get => _availableSpots;
            set
            {
                if (_availableSpots != value)
                {
                    _availableSpots = value;
                    OnPropertyChanged("AvailableSpots");
                }
            }
        }


        public TourEvent SelectedTourEvent
        {
            get => _selectedTourEvent;
            set
            {
                if (_selectedTourEvent != value)
                {
                    _selectedTourEvent = value;
                    OnPropertyChanged("SelectedTourEvent");
                }
            }
        }

        private TourPoint _tourPoint;

        public TourPoint TourPoint
        {
            get => _tourPoint;
            set
            {
                if (_tourPoint != value)
                {
                    _tourPoint = value;
                    OnPropertyChanged("TourPoint");
                }
            }
        }

        private Voucher _selectedVoucher;

        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                if (_selectedVoucher != value)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged("SelectedVoucher");
                }
            }
        }
        public int NumberOfPeople { get; set; }

        public TourPoint TourPointWhenGuestCame { get; set; }


        public ObservableCollection<TourEvent> TourEvents { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }

        public TourReservationWindow(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;

            _tourReservationController = new TourReservationController();
            _tourEventController = new TourEventController();
            _voucherController = new VoucherController();

            TourEvents = new ObservableCollection<TourEvent>(_tourEventController.GetTourEventsNotPassedForTour(tour));
            Vouchers = new ObservableCollection<Voucher>(_voucherController.VoucherForUser(SignInForm.LoggedUser));

            numberOfPeopleTextBox.Focus();

        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {

            if (AvailableSpots >= NumberOfPeople)
            {

                User user = SignInForm.LoggedUser;
                TourReservation existingTourReservation = _tourReservationController.GetTourReservationForTourEventAndUser(SelectedTourEvent.Id, user.Id);

                if (existingTourReservation != null)
                {
                    MessageBox.Show("Vec ste rezervisali ovu turu!");
                }
                else
                {
                    TourPointWhenGuestCame = new TourPoint { Id = -1 };
                    TourReservation tourReservation = new TourReservation(-1, NumberOfPeople, SelectedTourEvent, user, TourPointWhenGuestCame, SelectedVoucher, false);
                    _tourReservationController.Save(tourReservation);

                    if (SelectedVoucher != null)
                    {
                        Vouchers.Remove(SelectedVoucher);
                        SelectedVoucher = null;
                    }

                    MessageBox.Show("Uspesno ste rezervisali!");
                }
                this.Close();

            }
            else
            {
                MessageBox.Show("Nema dovoljno mesta!");
            }

            return;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefreshTours(List<TourEvent> tourEvents)
        {
            TourEvents.Clear();
            foreach (TourEvent tourEvent in tourEvents)
            {
                TourEvents.Add(tourEvent);
            }
        }

        private void Check_Availability_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourEvent == null)
            {
                return;
            }

            int reservedSpots = _tourEventController.CheckAvailability(SelectedTourEvent);
            AvailableSpots = SelectedTourEvent.Tour.MaxGuests - reservedSpots;

            if (AvailableSpots < NumberOfPeople)
            {
                AvailableSpotsText = "Nema dovoljno mesta";
                if (SelectedTourEvent == null)
                {
                    return;
                }
                List<TourEvent> tourEventsForLocation = _tourEventController.GetAvailableTourEventsForLocation(SelectedTourEvent.Tour.Location, NumberOfPeople);
                RefreshTours(tourEventsForLocation);
            }
            else
            {
                AvailableSpotsText = "Broj slobodnih:";
            }
        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Suggest_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourEvent == null)
            {
                return;
            }
            List<TourEvent> tourEventsForLocation = _tourEventController.GetAvailableTourEventsForLocation(SelectedTourEvent.Tour.Location, NumberOfPeople);
            RefreshTours(tourEventsForLocation);
        }
        

        private void VoucherReports_Click(object sender, RoutedEventArgs e)
        {
            // Create a new PDF document
            using (PdfDocument document = new PdfDocument())
            {
                // Add a page to the document
                PdfPage page = document.AddPage();

                // Create a graphics object for drawing on the page
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Set up the font
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                XFont titleFont = new XFont("Arial", 16, XFontStyle.Bold, options);
                XFont subtitleFont = new XFont("Arial", 12, XFontStyle.Bold, options);
                XFont contentFont = new XFont("Arial", 10, XFontStyle.Regular, options);

                // Get the voucher data
                List<Voucher> vouchers = _voucherController.VoucherForUser(SignInForm.LoggedUser);

                // Set up the page layout
                XRect pageBounds = new XRect(40, 40, page.Width - 80, page.Height - 80);
                XRect titleBounds = new XRect(pageBounds.Left, pageBounds.Top, pageBounds.Width, 30);
                XRect tableBounds = new XRect(pageBounds.Left, pageBounds.Top + 40, pageBounds.Width, pageBounds.Height - 40);


                // Draw the title
                gfx.DrawString("Voucher Reports", titleFont, XBrushes.Black, titleBounds, XStringFormats.Center);

                // Draw the table headers
                int headerXPos = (int)tableBounds.Left;
                int headerYPos = (int)tableBounds.Top;
                int columnWidth = (int)tableBounds.Width / 3;

                gfx.DrawString("Voucher ID", subtitleFont, XBrushes.Black, new XRect(headerXPos, headerYPos, columnWidth, 20), XStringFormats.CenterLeft);
                gfx.DrawString("Name", subtitleFont, XBrushes.Black, new XRect(headerXPos + columnWidth, headerYPos, columnWidth, 20), XStringFormats.CenterLeft);
                gfx.DrawString("Expiration Date", subtitleFont, XBrushes.Black, new XRect(headerXPos + (2 * columnWidth), headerYPos, columnWidth, 20), XStringFormats.CenterLeft);

                // Draw the voucher data
                int dataYPos = headerYPos + 20;
                foreach (Voucher voucher in vouchers)
                {
                    gfx.DrawString(voucher.Id.ToString(), contentFont, XBrushes.Black, new XRect(headerXPos, dataYPos, columnWidth, 20), XStringFormats.CenterLeft);
                    gfx.DrawString(voucher.Name, contentFont, XBrushes.Black, new XRect(headerXPos + columnWidth, dataYPos, columnWidth, 20), XStringFormats.CenterLeft);
                    gfx.DrawString(voucher.ExpirationDate.ToString("dd-MM-yyyy"), contentFont, XBrushes.Black, new XRect(headerXPos + (2 * columnWidth), dataYPos, columnWidth, 20), XStringFormats.CenterLeft);

                    dataYPos += 20;
                }

                // Save the PDF document
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\voucher_report.pdf";
                document.Save(filePath);

                // Open the PDF document with the default application
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }


    }
}
    

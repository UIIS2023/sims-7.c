using InitialProject.Controller;
using InitialProject.Domain.Models;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly ForumController _forumController;
        private readonly LocationController _locationController;

        public ObservableCollection<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }

        private readonly AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        public User guest { get; set; }




        private readonly GuestReviewController _guestReviewController;

        public string CleannessAverage { get; set; }
        public string BehaviorAverage { get; set; }




        public ProfileWindow(User user)
        {
            InitializeComponent();
            DataContext = this;

            guest = user;

            _forumController = new ForumController();
            _locationController = new LocationController();

            _accommodationReservationController = new AccommodationReservationController();

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetByUserId(guest.Id));

            Forums = new ObservableCollection<Forum>(_forumController.GetByAuthorId(guest.Id));

            for (int i = 0; i < Forums.Count; ++i)
            {
                Forums[i].Location = _locationController.GetAll().Find(a => a.Id == Forums[i].Location.Id);

            }




            _guestReviewController = new GuestReviewController();

            CleannessAverage = _guestReviewController.GetCleannessAverageByUserId(guest.Id);
            BehaviorAverage = _guestReviewController.GetBehaviorAverageByUserId(guest.Id);
        }

        private void CancelButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseForumButon_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedForum == null)
            {
                MessageBox.Show("Prvo morate odabrati forum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Forum forum = new Forum() { Id = SelectedForum.Id, Location = SelectedForum.Location, Author = SelectedForum.Author, IsOpen = false };
                _forumController.Update(forum);
                MessageBox.Show("Uspešno ste zatvorili izabrani forum!", "Zatvoremno!", MessageBoxButton.OK);
            }
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new PDF document
            Document document = new Document();

            // Set the file path for the generated PDF
            //string filePath = "Report.pdf";
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Report.pdf";

            // Create a new PDF writer to write the document to a file
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            // Open the PDF document
            document.Open();

            // Create a font for the report title
            Font titleFont = new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD);

            // Add the title to the document
            iTextSharp.text.Paragraph titleParagraph = new iTextSharp.text.Paragraph("Izvestaj o prosecnim ocenama", titleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            titleParagraph.SpacingAfter = 20f;
            document.Add(titleParagraph);

            // Add the content to the document
            document.Add(new iTextSharp.text.Paragraph($"Prosecna ocena cistoce: {CleannessAverage}"));
            document.Add(new iTextSharp.text.Paragraph($"Prosecna ocena postovanja pravila: {BehaviorAverage}"));

            // Close the PDF document
            document.Close();

            MessageBox.Show("Uspešno ste generisali PDF fajl!", "Izveštaj generisan!", MessageBoxButton.OK);
        }
    }
}

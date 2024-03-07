using InitialProject.Enumerations;
using InitialProject.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repositories
{
    public class ImageRepository
    {
        private const string FilePath = "../../../Resources/Data/images.csv";
        private static ImageRepository instance = null;

        private readonly Serializer<Image> _serializer;

        private List<Image> _images;

        private ImageRepository()
        {
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(FilePath);
        }
        public static ImageRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ImageRepository();
            }
            return instance;
        }



        public List<Image> GetAll()
        {
            return _images;
        }
        public Image Get(int id)
        {
            return _images.Find(i => i.Id == id);
        }
        public Image Save(Image image)
        {
            image.Id = NextId();
            _images.Add(image);
            _serializer.ToCSV(FilePath, _images);
            return image;
        }
        public int NextId()
        {
            if (_images.Count < 1)
            {
                return 1;
            }
            return _images.Max(i => i.Id) + 1;
        }
        public void Delete(Image image)
        {
            Image founded = _images.Find(i => i.Id == image.Id);
            _images.Remove(founded);
            _serializer.ToCSV(FilePath, _images);
        }

        public Image Update(Image image)
        {
            Image current = _images.Find(i => i.Id == image.Id);
            int index = _images.IndexOf(current);
            _images.Remove(current);
            _images.Insert(index, image);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _images);
            return image;
        }


        public void BindImageResource()
        {
            foreach (Image image in _images)
            {
                if (image.Resource == ImageResource.Tour)
                {
                    Tour tour = TourRepository.GetInstance().Get(image.ResourceId);
                    tour.Images.Add(image);
                }
                else
                {
                    Accommodation accommodation = AccommodationRepository.GetInstance().Get(image.ResourceId);
                    accommodation.Images.Add(image);
                }

            }
        }



    }
}

using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class ImageService
    {
        private ImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = ImageRepository.GetInstance();
        }

        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public Image Get(int id)
        {
            return _imageRepository.Get(id);
        }


        public Image Save(Image image)
        {
            return _imageRepository.Save(image);
        }
        public Image Update(Image image)
        {
            return _imageRepository.Update(image);
        }
        public void Delete(Image image)
        {
            _imageRepository.Delete(image);
        }

    }
}

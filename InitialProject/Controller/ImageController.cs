using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class ImageController
    {
        private readonly ImageService _imageService;

        public ImageController()
        {
            _imageService = new ImageService();
        }

        public List<Image> GetAll()
        {
            return _imageService.GetAll();
        }

        public Image Get(int id)
        {
            return _imageService.Get(id);
        }
        public Image Save(Image image)
        {
            return _imageService.Save(image);
        }
        public void Delete(Image image)
        {
            _imageService.Delete(image);
        }
        public void Update(Image image)
        {
            _imageService.Update(image);
        }
    }
}

using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ResourceId { get; set; }
        public string Description { get; set; }
        public ImageResource Resource { get; set; }
        public Image() { }
        public Image(int id, string url, int resourceId, string description, ImageResource resource)
        {
            Id = id;
            Url = url;
            ResourceId = resourceId;
            Description = description;
            Resource = resource;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                    Id.ToString(),
                    Url,
                    ResourceId.ToString(),
                    Description,
                    Resource.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Url = values[1];
            ResourceId = Convert.ToInt32(values[2]);
            Description = values[3];
            Resource = (ImageResource)Enum.Parse(typeof(ImageResource), values[4]);
        }
    }
}

using InitialProject.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";
        private static LocationRepository instance = null;

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        private LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }
        public static LocationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new LocationRepository();
            }
            return instance;
        }
        public List<Location> GetAll()
        {
            return _locations;
        }
        public Location Get(int id)
        {
            return _locations.Find(l => l.Id == id);

        }
        public Location Save(Location location)
        {

            location.Id = NextId();
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }
        public int NextId()
        {
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(l => l.Id) + 1;
        }
        public void Delete(Location location)
        {
            Location founded = _locations.Find(l => l.Id == location.Id);
            _locations.Remove(founded);
            _serializer.ToCSV(FilePath, _locations);
        }

        public Location Update(Location location)
        {
            Location current = _locations.Find(l => l.Id == location.Id);
            int index = _locations.IndexOf(current);
            _locations.Remove(current);
            _locations.Insert(index, location);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }
    }
}

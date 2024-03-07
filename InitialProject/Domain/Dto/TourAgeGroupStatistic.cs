using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class TourAgeGroupStatistic
    {
        public int To18 { get; set; }
        public int From18To50 { get; set; }
        public int From50 { get; set; }

        public TourAgeGroupStatistic(int to18, int from18To50, int from50)
        {
            To18 = to18;
            From18To50 = from18To50;
            From50 = from50;
        }
    }
}

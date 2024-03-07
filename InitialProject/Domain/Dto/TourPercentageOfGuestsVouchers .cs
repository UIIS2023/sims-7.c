using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class TourPercentageOfGuestsVouchers
    {

        public int PercentageWithVouchers { get; set; }
        public int PercentageWithoutVouchers { get; set; }
       

        public TourPercentageOfGuestsVouchers(int percentageWithVouchers, int percentageWithoutVouchers)
        {
            PercentageWithVouchers = percentageWithVouchers; 
            PercentageWithoutVouchers = percentageWithoutVouchers;
        }

    }
}

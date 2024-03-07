using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class TourRequestPercentageDto
    {
        public int PercentageOfAcceptedRequests { get; set; }
        public int PercentageOfRejectedRequests { get; set; }

        public int AverageNumberOfPeopleInAcceptedRequests { get; set; }

        public TourRequestPercentageDto(int percentageOfAcceptedRequests, int percentageOfRejectedRequests, int averageNumberOfPeopleInAcceptedRequests)
        {
            PercentageOfAcceptedRequests = percentageOfAcceptedRequests;
            PercentageOfRejectedRequests = percentageOfRejectedRequests;
            AverageNumberOfPeopleInAcceptedRequests = averageNumberOfPeopleInAcceptedRequests;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class AvailableTermsDto
    { 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public AvailableTermsDto(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public AvailableTermsDto() { }
    }
}

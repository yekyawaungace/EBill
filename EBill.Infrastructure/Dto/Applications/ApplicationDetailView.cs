using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.Dto.Applications
{
    public class ApplicationDetailView
    {
        public ApplicationsDto Application { get; set; }
        public List<AuditTrail> AuditTrail { get; set; }
        public List<Traveller> Traveller { get; set; }



    }

   
}

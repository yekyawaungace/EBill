using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelInsurance.Infrastructure.Dto.Audit
{
    public class AuditRequestModel
    {
        public bool Status { get; set; }
        public bool Endorsement { get; set; }
        public string CertificateId { get; set; }
        public string Remark { get; set; }
        public string ApplicationId { get; set; }

    }
}

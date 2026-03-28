using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Service.DTOs.Proposals
{
    public class CreateProposal
    {
        public int ProjectId { get; set; }
        public string CoverLetter { get; set; } = string.Empty;
        public decimal ProposedBudget { get; set; }
        public int EstimatedDays { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Freelancer info (shown to client when reviewing proposals)
        public string FreelancerId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class Proposal
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string FreelancerId { get; set; }
        public string CoverLetter { get; set; } = string.Empty;
        public decimal ProposedBudget { get; set; }
        public int EstimatedDays { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending;
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

        // ── Navigation Properties ─────────────────────────
        public Project Project { get; set; } = null!;
        public User Freelancer { get; set; } = null!;
        // Created only when this proposal is accepted
        public Contract Contract { get; set; } = null!;
    }
}

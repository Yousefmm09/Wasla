using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class Contract
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ProposalId { get; set; }
        
        public string ClientId { get; set; }
        public string FreelancerId { get; set; }
        public decimal AgreedBudget { get; set; }
        public ContractStatus Status { get; set; } = ContractStatus.Active;

        // Filled by Freelancer when delivering work
        public string? DeliveryNote { get; set; }
        // Filled by Client when opening a dispute
        public string? DisputeReason { get; set; }

        public  DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeliveredAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }

        // ── Navigation Properties ─────────────────────────
        public Project Project { get; set; } = null!;
        public Proposal Proposal { get; set; } = null!;
        public User Client { get; set; } = null!;
        public User Freelancer { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();
    }
}

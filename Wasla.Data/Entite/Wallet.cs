using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class Wallet
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        /// Available balance — can be withdrawn or used
        public decimal Balance { get; set; } = 0;
        /// Locked funds held in escrow for active contracts
        public decimal EscrowBalance { get; set; } = 0;
        /// Total = Balance + EscrowBalance
        public decimal TotalBalance => Balance + EscrowBalance;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        // ── Navigation Properties ─────────────────────────
        public User User { get; set; } = null!;
        public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();
    }
}

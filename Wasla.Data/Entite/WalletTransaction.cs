using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class WalletTransaction
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }

        /// Linked contract — null for TopUp transactions
        public int? ContractId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

        // ── Navigation Properties ─────────────────────────
        public Wallet Wallet { get; set; } = null!;
        // null for TopUp transactions
        public Contract? Contract { get; set; }
    }
}

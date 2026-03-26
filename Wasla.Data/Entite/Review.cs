using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class Review
    {
        public int Id { get; set; }
        public int ContractId { get; set; }

        // Who wrote the review
        public string ReviewerId { get; set; }
        // Who received the review
        public string RevieweeId { get; set; }

        /// Rating from 1 to 5
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // ── Navigation Properties ─────────────────────────
        public Contract Contract { get; set; } = null!;
        public User Reviewer { get; set; } = null!;
        public User Reviewee { get; set; } = null!;

    }
}

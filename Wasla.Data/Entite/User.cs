using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class User:IdentityUser
    {
        public List<string> Skills { get; set; } = new();

        // Calculated from Reviews
        public double Rating { get; set; } = 0.0;
        public int ReviewCount { get; set; } = 0;
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ── Navigation Properties ─────────────────────────
        public ICollection<Project> PostedProjects { get; set; } = new List<Project>();
        public ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
        public ICollection<Contract> FreelancerContracts { get; set; } = new List<Contract>();
        public ICollection<Contract> ClientContracts { get; set; } = new List<Contract>();
        public ICollection<Review> GivenReviews { get; set; } = new List<Review>();
        public ICollection<Review> ReceivedReviews { get; set; } = new List<Review>();
  
        public Wallet? Wallet { get; set; }
    }
}

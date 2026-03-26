using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Data.Entite
{
    public class Project
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
        public ProjectStatus Status { get; set; } = ProjectStatus.Open;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public User Client { get; set; } = null!;
        public ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
        public Contract? Contract { get; set; }
    }
}

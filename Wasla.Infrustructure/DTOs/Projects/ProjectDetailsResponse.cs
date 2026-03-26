namespace Wasla.Infrustructure.DTOs.Projects;

// Full details — used in GET /api/projects/{id}
public class ProjectDetailsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public int ProposalCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Nested client info
    public Guid ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public double ClientRating { get; set; }
}

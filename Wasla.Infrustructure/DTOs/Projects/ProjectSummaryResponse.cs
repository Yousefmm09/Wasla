namespace Wasla.Infrustructure.DTOs.Projects;

// Lightweight — used in paginated list GET /api/projects
public class ProjectSummaryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public int ProposalCount { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

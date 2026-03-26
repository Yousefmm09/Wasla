namespace Wasla.Service.DTOs.Projects;

// Lightweight — used in paginated list GET /api/projects
public class ProjectSummaryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public int ProposalCount { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}

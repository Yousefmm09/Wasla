namespace Wasla.Service.DTOs.Projects;

// Query params: GET /api/projects?category=Backend&minBudget=500&page=1
public class ProjectFilterRequest
{
    public string? Category { get; set; }
    public decimal? MinBudget { get; set; }
    public decimal? MaxBudget { get; set; }
    public List<string>? Skills { get; set; }

    // "Open" | "InProgress" | "Completed" | null = all
    public string? Status { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

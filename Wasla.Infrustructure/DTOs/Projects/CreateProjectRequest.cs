namespace Wasla.Infrustructure.DTOs.Projects;

public class CreateProjectRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
}

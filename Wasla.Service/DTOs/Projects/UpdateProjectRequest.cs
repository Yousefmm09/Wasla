namespace Wasla.Service.DTOs.Projects;

// All fields nullable — only send what changed
public class UpdateProjectRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public DateTimeOffset? Deadline { get; set; }
    public string? Category { get; set; }
    public List<string>? Skills { get; set; }
}

namespace Wasla.Service.DTOs.Proposals;

public class ProposalResponse
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;
    public decimal ProposedBudget { get; set; }
    public int EstimatedDays { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }

    // Freelancer info (shown to client when reviewing proposals)
    public int FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
    public double FreelancerRating { get; set; }
    public List<string> FreelancerSkills { get; set; } = new();
}

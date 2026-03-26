namespace Wasla.Infrustructure.DTOs.Contracts;

public class ContractResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal AgreedBudget { get; set; }
    public string? DeliveryNote { get; set; }
    public string? DisputeReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Project info
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;

    // Parties
    public Guid ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public Guid FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
}

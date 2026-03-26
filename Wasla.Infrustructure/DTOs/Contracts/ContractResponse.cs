namespace Wasla.Infrustructure.DTOs.Contracts;

public class ContractResponse
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal AgreedBudget { get; set; }
    public string? DeliveryNote { get; set; }
    public string? DisputeReason { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }

    // Project info
    public int ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;

    // Parties
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
}

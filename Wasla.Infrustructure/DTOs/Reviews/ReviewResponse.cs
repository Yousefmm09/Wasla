namespace Wasla.Infrustructure.DTOs.Reviews;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Who wrote it
    public Guid ReviewerId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;

    // Who received it
    public Guid RevieweeId { get; set; }
    public string RevieweeName { get; set; } = string.Empty;
}

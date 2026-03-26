namespace Wasla.Infrustructure.DTOs.Reviews;

public class ReviewResponse
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    // Who wrote it
    public int ReviewerId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;

    // Who received it
    public int RevieweeId { get; set; }
    public string RevieweeName { get; set; } = string.Empty;
}

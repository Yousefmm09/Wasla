namespace Wasla.Infrustructure.DTOs.Reviews;

public class SubmitReviewRequest
{
    /// Must be between 1 and 5
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}

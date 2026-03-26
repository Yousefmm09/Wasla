namespace Wasla.Service.DTOs.AI;

public class AIPriceSuggestionResponse
{
    public decimal SuggestedMin { get; set; }
    public decimal SuggestedMax { get; set; }
    public string Reasoning { get; set; } = string.Empty;
}

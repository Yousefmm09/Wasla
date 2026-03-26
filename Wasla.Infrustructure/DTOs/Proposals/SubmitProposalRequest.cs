namespace Wasla.Infrustructure.DTOs.Proposals;

public class SubmitProposalRequest
{
    public string CoverLetter { get; set; } = string.Empty;
    public decimal ProposedBudget { get; set; }
    public int EstimatedDays { get; set; }
}

namespace Wasla.Service.DTOs.Admin;

public class ResolveDisputeRequest
{
    // "Client" or "Freelancer" — who receives the escrowed funds
    public string FundsGoTo { get; set; } = string.Empty;
    public string AdminNote { get; set; } = string.Empty;
}

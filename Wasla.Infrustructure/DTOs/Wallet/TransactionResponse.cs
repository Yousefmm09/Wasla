namespace Wasla.Infrustructure.DTOs.Wallet;

public class TransactionResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }

    // "TopUp" | "Escrow" | "Release" | "Refund"
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // null for TopUp transactions
    public Guid? ContractId { get; set; }
    public DateTime CreatedAt { get; set; }
}

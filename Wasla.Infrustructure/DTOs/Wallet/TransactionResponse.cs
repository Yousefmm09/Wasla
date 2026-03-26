namespace Wasla.Infrustructure.DTOs.Wallet;

public class TransactionResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }

    // "TopUp" | "Escrow" | "Release" | "Refund"
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // null for TopUp transactions
    public int? ContractId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

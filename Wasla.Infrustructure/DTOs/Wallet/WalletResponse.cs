namespace Wasla.Infrustructure.DTOs.Wallet;

public class WalletResponse
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public decimal EscrowBalance { get; set; }
    public decimal TotalBalance { get; set; }
}

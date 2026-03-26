namespace Wasla.Service.DTOs.Wallet;

public class WalletResponse
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public decimal EscrowBalance { get; set; }
    public decimal TotalBalance { get; set; }
}

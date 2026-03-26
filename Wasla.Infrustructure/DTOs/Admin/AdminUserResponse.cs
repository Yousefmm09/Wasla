namespace Wasla.Infrustructure.DTOs.Admin;

public class AdminUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsBanned { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}

namespace Wasla.Service.DTOs.Admin;

public class AdminUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsBanned { get; set; }
    public double Rating { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

namespace Wasla.Infrustructure.DTOs.Auth;

public class UserProfileResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public double Rating { get; set; }= 0.0;
    public int ReviewCount { get; set; } = 0;
    public DateTimeOffset CreatedAt { get; set; }
}

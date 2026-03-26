namespace Wasla.Service.DTOs.Auth;

public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword {  get; set; } = string.Empty;
    public string PhoneNumber {  get; set; } = string.Empty;


    /// "Client" or "Freelancer"
    public string Role { get; set; } = string.Empty;

    // Required only when Role == "Freelancer"
    public List<string>? Skills { get; set; }
}

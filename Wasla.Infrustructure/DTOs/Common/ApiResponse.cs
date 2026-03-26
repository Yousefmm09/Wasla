namespace Wasla.Infrustructure.DTOs.Common;

// Standard envelope — wrap ALL controller responses in this
public record ApiResponse<T>(bool Success, string Message = "", T? Data = default, List<string>? Errors = null);

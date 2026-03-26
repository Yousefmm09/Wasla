using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.DTOs.Auth;
using Wasla.Service.DTOs.Common;

namespace Wasla.Service.Abstract.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterRequest>> RegisterAsync(RegisterRequest request);
        Task<ApiResponse<AuthResponse>> LoginAsync(string username, string password);
        Task<ApiResponse<AuthResponse>> RefreshTokenAsync(string refreshToken);
    }
}

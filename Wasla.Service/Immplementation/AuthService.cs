using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Data.Entite;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Auth;
using Wasla.Service.DTOs.Common;

namespace Wasla.Service.Immplementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthRepo _authRepo;

        public AuthService(UserManager<User> userManager, IAuthRepo authRepo)
        {
            _userManager=userManager;
            _authRepo=authRepo;
        }
        public Task<ApiResponse<AuthResponse>> LoginAsync(string username, string password)
        {
            var res= _authRepo.LoginAsync(username, password);
            return res;
        }

        public Task<ApiResponse<AuthResponse>> RefreshTokenAsync(string refreshToken)
        {
            var res= _authRepo.RefreshTokenAsync(refreshToken);
            return res;
        }

        public async Task<ApiResponse<RegisterRequest>> RegisterAsync(RegisterRequest request)
        {
            var res= await _authRepo.RegisterAsync(request);
            return res;
        }
    }
}

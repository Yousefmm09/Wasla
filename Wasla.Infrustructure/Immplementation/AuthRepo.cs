using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wasla.Data.Entite;
using Wasla.Infrustructure.Context;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.DTOs.Auth;
using Wasla.Service.DTOs.Common;

namespace Wasla.Infrustructure.Immplementation
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDb _appDb;
        private readonly IConfiguration _config;

        public AuthRepo(UserManager<User> userManager, AppDb appDb, IConfiguration config)
        {
            _userManager = userManager;
            _appDb = appDb;
            _config = config;
        }
        public async Task<ApiResponse<AuthResponse>> LoginAsync(string Email, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
                return new ApiResponse<AuthResponse>
                    (Success: false,
                    Message: "Invalid email or password",
                    Data: null,
                    Errors: new List<string> { "user not found" }
                    );
            var passwordValid = _userManager.CheckPasswordAsync(user, password).Result;
            if (!passwordValid)
                return new ApiResponse<AuthResponse>
                    (Success: false,
                    Message: "Invalid email or password",
                    Data: null,
                    Errors: new List<string> { "invalid password" }
                    );
            var token = CreatJwtToken(user).Result;
            var rfToken = CreateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                Token = rfToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = "127.0.0.1",
                RevokedByIp = "127.0.0.1",
                ReplacedByToken = null,
                UserId = user.Id,

            };
           await _appDb.AddAsync( refreshTokenEntity );
            await _appDb.SaveChangesAsync();
            var respones = new AuthResponse
            {
                AccessToken = token,
                RefreshToken = rfToken,
                Name = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
            return new ApiResponse<AuthResponse>
                    (Success: true,
                    Message: "Login successful",
                    Data: respones,
                    Errors: null
                    );
        
        }

        public async Task<ApiResponse<RegisterRequest>> RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
                return new ApiResponse<RegisterRequest>
                    (Success: false,
                    Message: "please enter another email",
                    Data: null,
                    Errors: new List<string> { "the email are found" }
                    );
            if (request.ConfirmPassword != request.Password)
                return new ApiResponse<RegisterRequest>
                   (Success: false,
                   Message: "The password and confirm password not match",
                   Data: null,
                   Errors: new List<string> { }
                   );
            var newUser = new User
            {
                UserName = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Skills = request.Skills,
                CreatedAt = DateTime.UtcNow,
                Role = request.Role
            };
            // create user and role

            var result = await _userManager.CreateAsync(newUser, request.Password);
            var roleResult = await _userManager.AddToRoleAsync(newUser, request.Role);
            var wallet = new Wallet
            {
                UserId = newUser.Id,
                Balance = 0,
                CreatedAt = DateTime.UtcNow,
                EscrowBalance = 0,
            };
            await _appDb.Wallets.AddAsync(wallet);
            await _appDb.SaveChangesAsync();
            if (!result.Succeeded)
                return new ApiResponse<RegisterRequest>
                    (Success: false,
                    Message: "Failed to create user",
                    Data: null,
                    Errors: result.Errors.Select(e => e.Description).ToList()
                    );
            return new ApiResponse<RegisterRequest>
                    (Success: true,
                    Message: "User created successfully",
                    Data: request,
                    Errors: null
                    );
        }
        // jwt 
        private Task<string> CreatJwtToken(User user)
        {
            // claims
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            claims.AddRange(_userManager.GetRolesAsync(user).Result.Select(role => new Claim(ClaimTypes.Role, role)));
            // creat key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryInMinutes"])),
                signingCredentials: creds
            );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        // rf Token
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        // validate rf token
        public async Task<ApiResponse<AuthResponse>> RefreshTokenAsync(string rftoken)
        {
            var token = await _appDb.RefreshTokens.AsNoTracking().Include(x => x.User).Where(x => x.Token == rftoken).FirstOrDefaultAsync();
            if (token == null ||  token.Expires < DateTime.UtcNow)
                throw new Exception("Invalid or expired refresh token");
            token.Revoked = DateTime.UtcNow;
            var newRfToken =  CreateRefreshToken();
            var refreshToken = new RefreshToken
            {
                Token = newRfToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = token.CreatedByIp,
                UserId = token.UserId
            };
            await _appDb.RefreshTokens.AddAsync(refreshToken);
            await _appDb.SaveChangesAsync();
            return new ApiResponse<AuthResponse>
                (Success: true,
                Message: "Token refreshed successfully",
                Data: new AuthResponse
                {
                    Name=token.User.UserName,
                    Email=token.User.Email,
                    Role=token.User.Role,
                    AccessToken = await CreatJwtToken(token.User),
                    RefreshToken = newRfToken
                },
                Errors: null
                );
        }
    }
}

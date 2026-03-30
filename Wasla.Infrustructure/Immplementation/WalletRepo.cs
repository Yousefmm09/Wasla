using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wasla.Data.Entite;
using Wasla.Infrustructure.Context;
using Wasla.Service.Abstract.Repositories;

namespace Wasla.Infrustructure.Immplementation
{
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDb _app;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WalletRepo(IHttpContextAccessor httpContextAccessor, AppDb appDb)
        {
            _httpContextAccessor = httpContextAccessor;
            _app = appDb;
        }

        public async Task<string> AddToWalletAsync(string userId, decimal amount)
        {
            var user = await _app.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var wallet = await _app.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (user == null)
                return "User not found";

                wallet.Balance += amount;
            wallet.UpdatedAt = DateTimeOffset.UtcNow;
            _app.Wallets.Update(wallet);
            await _app.SaveChangesAsync();
            return "Amount added to wallet successfully";
        }

        public async Task<decimal> GetWalletBalanceAsync()
        {
            var uId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (uId == null)
                throw new Exception("User not found, please login again");
            var user = await _app.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == uId);
            if (user == null)
                throw new Exception("User not found, please register") ;

            if (user == null || user.Wallet == null)
                return 0;
            return user.Wallet.Balance;
        }

        public async Task<string> SubtractFromWalletAsync(string userId, decimal amount)
        {
            var user = await _app.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || user.Wallet == null)
                throw new Exception("User or wallet not found");
            if (user.Wallet.Balance < amount)
                throw new Exception("Insufficient balance");
            user.Wallet.Balance -= amount;
            _app.Users.Update(user);
            await _app.SaveChangesAsync();
            return "Amount subtracted from wallet successfully";
        }
        // send money from one user to another
        public async Task<string> SendMoneyAsync(string senderId, string receiverId, decimal amount)
        {
            var sender = await _app.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == senderId);
            var receiver = await _app.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == receiverId);
            if (sender == null || sender.Wallet == null)
                throw new Exception("Sender or wallet not found");
            if (receiver == null || receiver.Wallet == null)
                throw new Exception("Receiver or wallet not found");
            if (sender.Wallet.Balance < amount)
                throw new Exception("Insufficient balance");
            sender.Wallet.Balance -= amount;
            receiver.Wallet.Balance += amount;
            sender.Wallet.UpdatedAt = DateTimeOffset.UtcNow;
            receiver.Wallet.UpdatedAt = DateTimeOffset.UtcNow;
            _app.Wallets.Update(sender.Wallet);
            _app.Wallets.Update(receiver.Wallet);
            await _app.SaveChangesAsync();
            return "Money sent successfully";
        }
    }
}

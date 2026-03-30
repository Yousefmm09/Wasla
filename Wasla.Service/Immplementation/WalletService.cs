using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.Abstract.Services;

namespace Wasla.Service.Immplementation
{
    public class WalletService : IWalletSerivce
    {
       private readonly IWalletRepo _walletRepo;
        public WalletService(IWalletRepo walletRepo)
        {
            _walletRepo = walletRepo;
        }

        public Task<string> AddToWalletAsync(string userId, decimal amount)
        {
            return _walletRepo.AddToWalletAsync(userId, amount);
        }
        public Task<decimal> GetWalletBalanceAsync()
        {
            return _walletRepo.GetWalletBalanceAsync();
        }

        public Task<string> SendMoneyAsync(string senderId, string receiverId, decimal amount)
        {
            return _walletRepo.SendMoneyAsync(senderId, receiverId, amount);
        }

        public Task<string> SubtractFromWalletAsync(string userId, decimal amount)
        {
            return _walletRepo.SubtractFromWalletAsync(userId, amount);
        }
    }
}

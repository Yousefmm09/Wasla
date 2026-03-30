using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Service.Abstract.Services
{
    public interface IWalletSerivce
    {
        Task<decimal> GetWalletBalanceAsync();
        Task<string> AddToWalletAsync(string userId, decimal amount);
        Task<string> SubtractFromWalletAsync(string userId, decimal amount);
        Task<string> SendMoneyAsync(string senderId, string receiverId, decimal amount);
    }
}

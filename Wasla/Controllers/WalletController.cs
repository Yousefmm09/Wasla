using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Auth;

namespace Wasla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletSerivce _wallet;
        public WalletController(IWalletSerivce wallet)
        {
            _wallet = wallet;
        }
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var res = await _wallet.GetWalletBalanceAsync();

            if (ModelState.IsValid)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost("Add_to_Wallet")]
        public async Task<IActionResult> AddToWallet(string userId, decimal amount)
        {
            var res = await _wallet.AddToWalletAsync(userId, amount);
            if (ModelState.IsValid)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost("Subtract_from_Wallet")]
        public async Task<IActionResult> SubtractFromWallet(string userId, decimal amount)
        {
            var res = await _wallet.SubtractFromWalletAsync(userId, amount);
            if (ModelState.IsValid)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost("Send_Money")]
        public async Task<IActionResult> SendMoney(string senderId, string receiverId, decimal amount)
        {
            var res = await _wallet.SendMoneyAsync(senderId, receiverId, amount);
            if (ModelState.IsValid)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}

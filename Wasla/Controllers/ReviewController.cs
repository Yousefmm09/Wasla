using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Auth;
using Wasla.Service.DTOs.Reviews;

namespace Wasla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewSerivce _reviewService;
        public ReviewController(IReviewSerivce reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost("Create-Review")]
        public async Task<IActionResult> CreateReview(ReviewResponse response)
        {
            var result = await _reviewService.CreateReview(response);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);

        }
    }
}

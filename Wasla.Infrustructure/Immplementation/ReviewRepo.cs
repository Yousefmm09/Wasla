using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wasla.Api;
using Wasla.Data.Entite;
using Wasla.Infrustructure.Context;
using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Reviews;

namespace Wasla.Infrustructure.Immplementation
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly AppDb _appDb;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewRepo(AppDb appDb, IHttpContextAccessor httpContextAccessor)
        {
            _appDb = appDb;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<ReviewResponse>> CreateReview(ReviewResponse response)
        {
            var Client = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Client == null)
                return new ApiResponse<ReviewResponse>
                (
                    Success: false,
                    Message: "Reviewer not found",
                    Data: null,
                    Errors: new List<string> { "Reviewer not found" }
                );
            var review = new Review
            {
                Rating = response.Rating,
                Comment = response.Comment,
                CreatedAt = response.CreatedAt,
                ReviewerId = Client,
                RevieweeId = response.RevieweeId
            };
          await  _appDb.Reviews.AddAsync(review);
            await _appDb.SaveChangesAsync();
            return new ApiResponse<ReviewResponse>
            (
                Success: true,
                Message: "Review created successfully",
                Data: response,
                Errors: null
            );

        }
    }
}

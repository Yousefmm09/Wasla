using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Api;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Reviews;

namespace Wasla.Service.Immplementation
{
    public class ReviewService : IReviewSerivce
    {
        private readonly IReviewRepo _reviewRepo;
        public ReviewService(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }
        public Task<ApiResponse<ReviewResponse>> CreateReview(ReviewResponse response)
        {
            return _reviewRepo.CreateReview(response);
        }
    }
}

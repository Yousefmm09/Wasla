using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Reviews;

namespace Wasla.Api
{
    public interface IReviewRepo
    {
        Task <ApiResponse<ReviewResponse>> CreateReview(ReviewResponse response);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Reviews;

namespace Wasla.Service.Abstract.Services
{
    public interface IReviewSerivce
    {
        Task<ApiResponse<ReviewResponse>> CreateReview(ReviewResponse response);

    }
}

using Interfaces.DataContracts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ServiceContracts
{
    [ServiceContract]
    public interface IReviewService
    {
        [OperationContract]
        string CreateReview(Review review);

        [OperationContract]
        Review GetReview(string reviewId);

        [OperationContract]
        IEnumerable<Review> GetUserReviews(string userId);
    }
}

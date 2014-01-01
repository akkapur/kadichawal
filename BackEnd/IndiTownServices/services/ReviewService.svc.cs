using Database.DAC.CRUD;
using Interfaces.DataContracts;
using Interfaces.ServiceContracts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IndiTownServices.services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReviewService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReviewService.svc or ReviewService.svc.cs at the Solution Explorer and start debugging.
    public class ReviewService : IReviewService
    {
        public string CreateReview(Interfaces.DataContracts.Review review)
        {
            IReviewCRUD<Review> reviewCrud = new ReviewCRUD<Review>();
            reviewCrud.Initialize();
            reviewCrud.Create(review);
            return review.ReviewId;
        }

        public Review GetReview(string reviewId)
        {
            IReviewCRUD<Review> reviewCrud = new ReviewCRUD<Review>();
            reviewCrud.Initialize();
            return reviewCrud.Read(reviewId);
        }

        public IEnumerable<Review> GetUserReviews(string userId)
        {
            IReviewCRUD<Review> reviewCrud = new ReviewCRUD<Review>();
            reviewCrud.Initialize();
            return reviewCrud.Read(x => x.ReviewerId == userId);
        }

        public IEnumerable<Review> GetBusinessReview(string organizationId)
        {
            IReviewCRUD<Review> reviewCrud = new ReviewCRUD<Review>();
            reviewCrud.Initialize();
            return reviewCrud.Read(x => x.OrganizationId == organizationId);            
        }
    }
}

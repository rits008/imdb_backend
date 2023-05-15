using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1.Repository;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;
using Assignment1.Models.Db;

namespace Assignment1.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public IList<ReviewResponse> Get()
        {
            IList<Review> reviews = _reviewRepository.Get();
            return reviews.Select(a => new ReviewResponse
            {
                Id = a.Id,
                Message = a.Message,
                MovieId = a.MovieId
            }).ToList();
        }
        public ReviewResponse Get(int id)
        {
            var review = _reviewRepository.Get(id);
            if(review==null)
            {
                throw new Exception("reveiw Not found");
            }
            return new ReviewResponse { Id = id, Message = review.Message, MovieId = review.MovieId };
        }
        public void Create(ReviewRequest reviewRequest)
        {
            if(string.IsNullOrEmpty(reviewRequest.Message))
            {
                throw new Exception("Reveiw message is Required");
            }
            _reviewRepository.Create(reviewRequest);
        }
        public void Update(int id, int movieId,ReviewRequest reviewRequest)
        {
            if (string.IsNullOrEmpty(reviewRequest.Message))
            {
                throw new Exception("Reveiw message is Required");
            }
            var review = _reviewRepository.Get(id);
            if (review == null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _reviewRepository.Update(id,movieId,reviewRequest);
        }
        public void Delete(int id)
        {
            var reviewResponse = _reviewRepository.Get(id);
            if(reviewResponse==null)
            {
                throw new ArgumentNullException($"Not Found");
            }
            _reviewRepository.Delete(id);
        }
    }
}

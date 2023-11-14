using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IReviewService
    {
        Review GetById(int id);
        List<Review> GetReviewsForAnime(int animeId);
        void Insert(Review review);
        void Update(Review review);
        void Delete(Review review);
    }
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;


        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review GetById(int id)
        {
            return _reviewRepository.GetById(id);
        }

        public List<Review> GetReviewsForAnime(int animeId)
        {
            return _reviewRepository.GetReviewsForAnime(animeId);
        }

        public void Insert(Review review)
        {
            _reviewRepository.Insert(review);
        }
        public void Update(Review review)
        {
            _reviewRepository.Update(review);
        }
        public void Delete(Review review)
        {
            _reviewRepository.Delete(review);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository; // Підключення до репозиторію відгуків

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public List<Review> GetReviewsByAnimeId(int animeId)
        {
            return _reviewRepository.GetReviewsForAnime(animeId);
        }

        public void AddReview(Review review)
        {
            _reviewRepository.Add(review);
        }

        // Інші методи для роботи з відгуками
    }
}

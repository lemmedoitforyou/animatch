using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;
using AniBLL.Models;
using Microsoft.EntityFrameworkCore;

namespace AniBLL.Services
{
    public interface IReviewService
    {
        ReviewModel GetById(int id);
        List<ReviewModel> GetReviewsForAnime(int animeId);
        void Insert(ReviewModel review);
        void Update(ReviewModel review);
        void Delete(int review);
        int GetLastId();
    }
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;


        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public ReviewModel GetById(int id)
        {
            var review = _reviewRepository.GetById(id);
            ReviewModel temp = new ReviewModel
            {
                Id = review.Id,
                AnimeId = review.AnimeId,
                Rate = review.Rate,
                Text = review.Text,
                UserId = review.UserId
            };
            return temp;
        }

        public List<ReviewModel> GetReviewsForAnime(int animeId)
        {
            List<Review> reviewRepository = _reviewRepository.GetReviewsForAnime(animeId);


            List<ReviewModel> reviewForAnime = reviewRepository
                .Select(review => new ReviewModel
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    AnimeId = review.AnimeId,
                    Text = review.Text,
                    Rate = review.Rate,

                })
                .ToList();

            return reviewForAnime;
        }

        public void Insert(ReviewModel review)
        {
            _reviewRepository.Insert(review);
        }
        public void Update(ReviewModel review)
        {
            _reviewRepository.Update(review);
        }
        public void Delete(int review)
        {
            _reviewRepository.Delete(review);
        }

        public int GetLastId()
        {
            return _reviewRepository.GetLastId();
        }
    }
}

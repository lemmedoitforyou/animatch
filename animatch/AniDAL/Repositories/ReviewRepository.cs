using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IReviewRepository
    {
        Review GetById(int id);
        List<Review> GetReviewsForAnime(int animeId);
        void Add(Review review);
        void Update(Review review);
        void Delete(Review review);
    }
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Review GetById(int id)
        {
            return _context.Review.FirstOrDefault(r => r.Id == id);
        }

        public List<Review> GetReviewsForAnime(int animeId)
        {
            return _context.Review.Where(r => r.AnimeId == animeId).ToList();
        }

        public void Add(Review review)
        {
            _context.Review.Add(review);
            _context.SaveChanges();
        }

        public void Update(Review review)
        {
            _context.Review.Update(review);
            _context.SaveChanges();
        }

        public void Delete(Review review)
        {
            _context.Review.Remove(review);
            _context.SaveChanges();
        }
    }

}

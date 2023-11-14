using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IReviewRepository: IGenericRepository<Review>
    {
        List<Review> GetReviewsForAnime(int animeId);
    }
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public List<Review> GetReviewsForAnime(int animeId)
        {
            return _context.Review.Where(r => r.AnimeId == animeId).ToList();
        }
    }

}

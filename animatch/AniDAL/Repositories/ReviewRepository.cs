using AniDAL.DataBaseClasses;

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
        public int GetLastUserId()
        {
            int lastReviewId = _context.Review.Max(u => u.Id);
            return lastReviewId;
        }   
    }
}

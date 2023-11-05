namespace AniDAL.DataBaseClasses
{
    public class LikedAnime
    {
        public int UserId { get; set; }

        public int AnimeId { get; set; }

        public UserInfo UserInfo { get; set; }

        public Anime Anime { get; set; }
    }

}

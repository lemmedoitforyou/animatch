namespace AniDAL.DataBaseClasses
{
    public class AnimeGenres
    {
        public int AnimeId { get; set; }

        public int GenreId { get; set; }

        public Anime Anime { get; set; }

        public Genres Genre { get; set; }
    }

}

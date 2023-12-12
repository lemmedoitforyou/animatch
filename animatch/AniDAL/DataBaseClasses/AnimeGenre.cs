using System.ComponentModel.DataAnnotations;

namespace AniDAL.DataBaseClasses
{
    public class AnimeGenre
    {
        [Key]
        public int Id { get; set; }

        public int AnimeId { get; set; }

        public int GenreId { get; set; }
    }
}

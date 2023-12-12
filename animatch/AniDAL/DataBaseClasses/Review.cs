using System.ComponentModel.DataAnnotations;

namespace AniDAL.DataBaseClasses
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AnimeId { get; set; }

        public string Text { get; set; }

        public int Rate { get; set; }
    }
}

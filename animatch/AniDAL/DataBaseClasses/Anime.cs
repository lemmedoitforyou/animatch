using System.ComponentModel.DataAnnotations;

namespace AniDAL.DataBaseClasses
{
    public class Anime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Text { get; set; }

        public double Imdbrate { get; set; }

        [MaxLength(255)]
        public string Photo { get; set; }

        public int Year { get; set; }
    }
}
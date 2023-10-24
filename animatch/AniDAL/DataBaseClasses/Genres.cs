using System.ComponentModel.DataAnnotations;

namespace AniDAL.DataBaseClasses
{
    public class Genres
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}

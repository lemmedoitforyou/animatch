using System.ComponentModel.DataAnnotations;

namespace AniDAL.DataBaseClasses
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}

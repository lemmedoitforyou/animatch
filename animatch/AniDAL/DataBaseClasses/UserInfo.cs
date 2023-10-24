using System.ComponentModel.DataAnnotations;

namespace AniDAL.DataBaseClasses
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public int Level { get; set; }

        public string Text { get; set; }

        [MaxLength(255)]
        public string Photo { get; set; }

        public int WatchedCount { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models.DTO
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Number { get; set; }
        public string Username { get; set; }
    }
}

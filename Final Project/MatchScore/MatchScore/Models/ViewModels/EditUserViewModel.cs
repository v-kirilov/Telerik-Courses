using System.ComponentModel.DataAnnotations;

namespace MatchScore.Models.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}

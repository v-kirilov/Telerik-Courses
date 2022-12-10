using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MatchScore.Models.ViewModels
{
    public class CreatePlayerViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string SportClub { get; set; }

        public string NewCountry { get; set; }
        public string NewSportClub { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
    }
}

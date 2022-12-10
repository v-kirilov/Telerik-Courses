using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MatchScore.Models.ViewModels
{
    public class EditPlayerViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Country { get; set; }

        public string SportClub { get; set; }

        public string NewCountry { get; set; }
        public string NewSportClub { get; set; }

        public string CurrentProfileImageUrl { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile NewProfileImage { get; set; }
    }
}

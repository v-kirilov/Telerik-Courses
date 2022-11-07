using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.Users.UsersViewModels
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Username = user.Username;
            this.Password = user.Password;
            this.Role = user.Role.Name;
            if (user.Role.Name == "Admin")
            {
                if (user.PhoneNumber != null)
                {
                    this.PhoneNumber = user.PhoneNumber.Number;
                }
                else
                {
                    this.PhoneNumber = "None";
                }
            }
            else
            {
                this.PhoneNumber = null;
            }

            this.IsBlocked = user.IsBlocked;
        }

        [MaxLength(32, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string FirstName { get; set; }

        [MaxLength(32, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsBlocked { get; set; }

        [Display(Name = "Profile Picture")]  
        public IFormFile ProfileImage { get; set; }
    }
}


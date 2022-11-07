using ForumSystem.Models.DTO.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO
{
    public class UserDto : IUserDto
    {
        public UserDto(User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Username = user.Username;
            this.IsBlocked = user.IsBlocked;
            this.Role = user.Role.Name;
        }

        [JsonProperty(Order = 0)]
        public int Id { get; set; }

        [JsonProperty(Order = 1)]
        public string FirstName { get; set; }

        [JsonProperty(Order = 2)]
        public string LastName { get; set; }

        [JsonProperty(Order = 3)]
        public string Email { get; set; }

        [JsonProperty(Order = 4)]
        public string Username { get; set; }

        [JsonProperty(Order = 5)]
        public bool IsBlocked { get; set; }

        [JsonProperty(Order = 6)]
        public string Role { get; set; }
    }
}

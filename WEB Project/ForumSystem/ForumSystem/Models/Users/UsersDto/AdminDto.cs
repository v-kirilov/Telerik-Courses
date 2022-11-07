using ForumSystem.Models.DTO.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO
{
    public class AdminDto : UserDto, IUserDto
    {
        public AdminDto(User user)
            : base(user)
        {
            if (user.PhoneNumber != null)
            {
                this.PhoneNumber = user.PhoneNumber.Number;
            }
            else
            {
                this.PhoneNumber = "none";
            }
        }

        [JsonProperty(Order = 7)]
        public string PhoneNumber { get; set; }
    }
}

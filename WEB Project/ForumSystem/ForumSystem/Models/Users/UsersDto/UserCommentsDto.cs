using ForumSystem.Models.DTO.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO
{
    public class UserCommentsDto : UserDto, IUserDto
    {
        public UserCommentsDto(User user)
            : base(user)
        {
            this.Comments = user.Comments.Select(c => new CommentsUserDto(c)).ToList();
        }

        [JsonProperty(Order = 7)]
        public List<CommentsUserDto> Comments  { get; set; }
    }
}

using ForumSystem.Helpers;
using ForumSystem.Models.DTO.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO
{
    public class UserPostsDto : UserDto, IUserDto
    {
        public UserPostsDto(User user)
            : base(user)
        {
            this.Posts = user.Posts.Select(p => new PostsUserDto(p)).ToList();
        }

        [JsonProperty(Order = 7)]
        public List<PostsUserDto> Posts { get; set; }
    }
}

using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystem.Services.Contracts
{
    public interface IPostsService
    {
        List<PostDto> GetAll();
        PaginatedList<PostDto> FilterBy(PostQueryParameters filterPar);
        PostDto GetById(int id);
        PostDto Create(Post post);
        PostDto Update(int id, PostDto postDto);
        void DeletePost(int id);
        PostReactionDto CreatePostReaction(int id,PostReaction postReact);
        PostReactionDto UpdatePostReaction(int postId,string username, PostReaction postReact);
        void DeletePostReaction(int postId, User author);
        bool IsAuthor(User user, int id);
        bool IsAuthorOrAdmin(User user, int id);


    }
}

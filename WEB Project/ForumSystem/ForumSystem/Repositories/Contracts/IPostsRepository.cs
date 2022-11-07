using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystem.Repositories.Contracts
{
    public interface IPostsRepository
    {
        List<Post> GetAll();
        PaginatedList<Post> FilterBy(PostQueryParameters filterPar);
        Post GetById(int id);
        Post GetByTitle(string name);
        Post CreatePost(Post post);
        Post UpdatePost(int id, PostDto postDto);
        void Delete(int id);
        int GetIdOfPost(int id);
        int GetIdOfReaction(int reactionId, int postId);
        PostReaction CreatePostReaction(PostReaction postReact);
        PostReaction UpdatePostReaction(int postId, string username, PostReaction postReaction);

        PostReaction GetReactionById(int id, Post post);
        void DeleteReaction(PostReaction pr);

        PostReaction GetReactionByUsername(Post post, string username);

    }
}

using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystem.Services
{
    public class PostsService : IPostsService
    {
        private const string MissingPostWithThisId = "There is no posts with that Id!";

        private readonly IPostsRepository postsRepository;
        private readonly IModelMapper modelMapper;
        public PostsService(IPostsRepository postsRepository, IModelMapper modelMapper)
        {
            this.modelMapper = modelMapper;
            this.postsRepository = postsRepository;
        }
        public PostDto Create(Post post)
        {
            Post createPost = this.postsRepository.CreatePost(post);

            return modelMapper.MapPost(createPost);
        }

        public PostReactionDto CreatePostReaction(int id, PostReaction postReact)
        {
            bool postExists = false;

            try
            {
                if (this.postsRepository.GetIdOfPost(postReact.PostId) == id)
                {
                    postExists = true;
                }
            }
            catch (EntityNotFoundException)
            {
                postExists = false;
            }
            if (!postExists)
            {
                throw new InvalidDataException();
            }

            int userId = (int)postReact.UserId;
            Post postToReact = this.postsRepository.GetById(id);
            if (postToReact.Reactions.FirstOrDefault(x => x.UserId == userId) != null)
            {
                
                var updatedPostReaction = this.postsRepository.UpdatePostReaction(id,postReact.User.Username,postReact);
                //throw new DuplicateEntityException("This user has already reacted to this post!");
                return null;
            }

            PostReaction createdPostReaction = this.postsRepository.CreatePostReaction(postReact);
            return modelMapper.MapPostReaction(createdPostReaction);
        }



        public void DeletePost(int id)
        {
            Post post = this.postsRepository.GetById(id);

            this.postsRepository.Delete(id);
        }

        public void DeletePostReaction(int postId, User author)
        {

            Post post = this.postsRepository.GetById(postId);

            PostReaction pr = this.postsRepository.GetReactionByUsername(post, author.Username);

            this.postsRepository.DeleteReaction(pr);
        }

        public PaginatedList<PostDto> FilterBy(PostQueryParameters filterPar)
        {
            PaginatedList<Post> filteredList = this.postsRepository
                .FilterBy(filterPar);
            if (filteredList.Count==0)
            {
                throw new EntityNotFoundException("There are no posts matching your query.");
            }
            List<PostDto> helpListDto = new List<PostDto>(filteredList.Select(x => modelMapper.MapPost(x))).ToList();

            PaginatedList<PostDto> result = new PaginatedList<PostDto>(helpListDto, filteredList.TotalPages,filteredList.PageNumber);
            return result;
        }

        public List<PostDto> GetAll()
        {
            List<Post> result = this.postsRepository.GetAll();
            return result
                .Select(x => modelMapper.MapPost(x))
                .ToList();

        }

        public PostDto GetById(int id)
        {
            Post postToReturn = this.postsRepository.GetById(id);

            PostDto result = modelMapper.MapPost(postToReturn);
            return result;
        }

        public PostDto Update(int id, PostDto postDto)
        {
            bool duplicateExists = true;
            try
            {
                if (this.postsRepository.GetIdOfPost(postDto.Id) == id)
                {
                    duplicateExists = false;
                }
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }
            if (duplicateExists)
            {
                throw new DuplicateEntityException();
            }
            Post updatePost = this.postsRepository.UpdatePost(id, postDto);
            return modelMapper.MapPost(updatePost);
        }

        public PostReactionDto UpdatePostReaction(int postId, string username, PostReaction postReact)
        {
            bool postExists = false;
            bool reactExists = false;
            try
            {
                Post post = this.postsRepository.GetById(postId);
                postExists = true;
                if (post.Reactions.FirstOrDefault(r => r.User.Username == username) != null)
                {
                    reactExists = true;
                }
            }
            catch (EntityNotFoundException)
            {
                postExists = false;
                reactExists = false;
            }
            if (!postExists || !reactExists)
            {
                throw new InvalidDataException("Post or reaction does not exist!");
            }

            PostReaction updatedPostReaction = this.postsRepository.UpdatePostReaction(postId, username, postReact);
            return modelMapper.MapPostReaction(updatedPostReaction);
        }

        public bool IsAuthor(User author, int id)
        {
            if (author.Posts.FirstOrDefault(p => p.Id == id) != null)
            {
                return true;
            }
            else
            {
                throw new UnauthorizedOperationException("You are not the author!");
            }
        }
        public bool IsAuthorOrAdmin(User author, int id)
        {
            if (author.Posts.FirstOrDefault(p => p.Id == id) != null)
            {
                return true;
            }
            else if (author.Role.Name.Equals("Admin"))
            {
                return true;
            }
            else
            {
                throw new UnauthorizedOperationException("You are not the author or you are not Admin!");
            }
        }
    }
}

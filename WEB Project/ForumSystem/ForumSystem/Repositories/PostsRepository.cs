using ForumSystem.Data;
using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.Enums;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystem.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationContext context;
        private readonly IModelMapper modelMapper;


        public PostsRepository(ApplicationContext context, IModelMapper modelMapper)
        {
            this.context = context;
            this.modelMapper = modelMapper;

        }

        public List<Post> GetAll()
        {
            return this
                .GetPosts()
                .ToList();
        }

        public PaginatedList<Post> FilterBy(PostQueryParameters filterPar)
        {
            List<Post> result = this.GetAll();
            if (!string.IsNullOrEmpty(filterPar.Title))
            {
                result = result.FindAll(x => x.Title.Contains(filterPar.Title));
            }
            if (filterPar.UserId.HasValue)
            {
                result = result.FindAll(x => x.UserId == filterPar.UserId);
            }
            if (!string.IsNullOrEmpty(filterPar.User))
            {
                result = result.FindAll(x => x.User.Username == filterPar.User);
            }
            if (!string.IsNullOrEmpty(filterPar.Content))
            {
                result = result.FindAll(x => x.Content.Contains(filterPar.Content));
            }
            if (filterPar.HasMostPopular)
            {
                result = result.OrderByDescending(x => x.Comments.Count).Take(10).ToList();
            }
            if (filterPar.HasMostRecent)
            {
                result = result.OrderByDescending(x => x.Id).Take(10).ToList();
            }

            double totalPages = Math.Ceiling((1.0*result.Count())/filterPar.PageSize);

            result = Paginate(result, filterPar.PageNumber, filterPar.PageSize);

            return new PaginatedList<Post>(result.ToList(), (int)totalPages,filterPar.PageNumber);
        }

        private List<Post> Paginate(List<Post> result, int pageNumber, int pageSize)
        {
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Post CreatePost(Post post)
        {
            this.context.Add(post);
            this.context.SaveChanges();
            return post;
        }

        public PostReaction CreatePostReaction(PostReaction postReaction)
        {

            this.context.Add(postReaction);
            this.context.SaveChanges();
            return postReaction;
        }

        public void Delete(int id)
        {
            Post post = this.GetPosts()
                .FirstOrDefault(x => x.Id == id);
            this.context.Remove(post);
            this.context.SaveChanges();
        }
        public void DeleteReaction(PostReaction pr)
        {
            this.context.Remove(pr);
            this.context.SaveChanges();
        }


        public Post GetById(int id)
        {
            Post postToReturn = this.GetPosts()
                .FirstOrDefault(x => x.Id == id);
            return postToReturn ?? throw new EntityNotFoundException($"There is no post with id:{id}");
        }
        public PostReaction GetReactionById(int id, Post post)
        {
            PostReaction pr = post.Reactions.FirstOrDefault(r => r.Id == id);
            return pr ?? throw new EntityNotFoundException($"This post contains no reaction with id:{id}");
        }

        public Post GetByTitle(string title)
        {
            Post post = this.GetAll()
                .FirstOrDefault(x => x.Title == title);
            return post ?? throw new EntityNotFoundException($"There is no post with title:{title}");
        }

        public Post UpdatePost(int id, PostDto post)
        {
            Post postToUpdate = this.GetPosts()
                .FirstOrDefault(x => x.Id == id);
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            this.context.Update(postToUpdate);
            this.context.SaveChanges();

            return postToUpdate;
        }

        public PostReaction UpdatePostReaction(int postId, string username, PostReaction postReaction)
        {
            Post post = this.GetPosts().FirstOrDefault(x => x.Id == postId);
            PostReaction reactionToUpdate = post.Reactions.FirstOrDefault(x => x.User.Username == username);
            if (postReaction.Reaction==reactionToUpdate.Reaction)
            {
                DeleteReaction(reactionToUpdate);
                return null;
            }
            reactionToUpdate.Reaction = postReaction.Reaction;
            this.context.Update(reactionToUpdate);
            this.context.SaveChanges();

            return reactionToUpdate;
        }

        private IQueryable<Post> GetPosts()
        {
            return this.context.Posts
                .Include(x => x.Reactions)
                 .ThenInclude(u => u.User)
                .Include(u => u.User)
                .Include(prop => prop.Comments)
                .ThenInclude(u => u.User)
                .ThenInclude(u => u.CommentReactions);
            //.Include(prop => prop.Reactions);
        }
        public int GetIdOfPost(int id)
        {
            Post post = this.GetPosts().FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                throw new EntityNotFoundException($"There is no post with id:{id}");
            }
            return post.Id;
        }
        public int GetIdOfReaction(int reactionId, int postId)
        {
            Post post = GetPosts().ToList().FirstOrDefault(p => p.Id == postId);
            PostReaction react = post.Reactions.FirstOrDefault(x => x.Id == reactionId);
            if (react == null)
            {
                throw new EntityNotFoundException($"There is no reaction with id:{reactionId}");
            }
            return react.Id;
        }

        public PostReaction GetReactionByUsername(Post post, string username)
        {
            PostReaction pr = post.Reactions.FirstOrDefault(r => r.User.Username == username);
            return pr ?? throw new EntityNotFoundException($"There is no reaction made by user with username:{username}");
        }
    }
}

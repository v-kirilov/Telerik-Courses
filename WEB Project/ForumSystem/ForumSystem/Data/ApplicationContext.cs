using ForumSystem.Models;
using ForumSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        // 1. Configure SQL Database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }

        // 2. Seed database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed the database with users
            List<User> users = new List<User>()
            {
                new User
                {
                    Id = 1,
                    FirstName = "Bruce",
                    LastName = "Banner",
                    Email = "bruce@avengers.com",
                    Username = "bruce123",
                    Password = "123",
                    RoleId = 2
                },
                new User
                {
                    Id = 2,
                    FirstName = "Tony",
                    LastName = "Stark",
                    Email = "tony@avengers.com",
                    Username = "tony111",
                    Password = "321",
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    FirstName = "Gosho",
                    LastName = "Petrov",
                    Email = "g.pertrov@gmail.com",
                    Username = "gogo3",
                    Password = "111",
                    RoleId = 1
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            // Seed the database with roles
            List<Role> roles = new List<Role>()
            {
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "Default"
                },
                new Role
                {
                    Id = 3,
                    Name = "Moderator"
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);

            // Seed the database with comments
            List<Comment> comments = new List<Comment>()
            {
                new Comment
                {
                    Id = 1,
                    CommentContent = "This post is very helpful.",
                    PostId = 2,
                    UserId = 1,
                },
                new Comment
                {
                    Id = 2,
                    CommentContent = "Great post!",
                    PostId = 1,
                    UserId = 2,
                },
                new Comment
                {
                    Id = 3,
                    CommentContent = "I don't like this post!",
                    PostId = 3,
                    UserId = 1,
                },
                new Comment
                {
                    Id = 4,
                    CommentContent = "I really like this post!",
                    PostId = 4,
                    UserId = 2,
                }

            };
            modelBuilder.Entity<Comment>().HasData(comments);

            // Seed the database with phone numbers
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber
                {
                    Id = 1,
                    Number = "1111",
                    UserId = 3
                }
            };
            modelBuilder.Entity<PhoneNumber>().HasData(phoneNumbers);

            //Seed the database with posts
            List<Post> posts = new List<Post>
            {
                 new Post
                {
                    Id = 1,
                    UserId = 1,
                    Title="What is Lorem Ipsum?",
                    Content="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book."
                },
                new Post
                {
                    Id = 2,
                    UserId = 1,
                    Title="Why do we use it?",
                    Content = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English."
                },
                new Post
                {
                    Id = 3,
                    UserId = 2,
                    Title="Get this post",
                    Content="Get this content for my fisrt post"
                },
                new Post
                {
                    Id = 4,
                    UserId = 2,
                    Title="Where does it come from?",
                    Content="Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source"

                },
                new Post
                {
                    Id = 5,
                    UserId = 3,
                    Title="Where can I get some?",
                    Content="There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text."

                },
                new Post
                {
                    Id = 6,
                    UserId = 3,
                    Title="The standard Lorem Ipsum passage, used since the 1500s",
                    Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                }
            };
            modelBuilder.Entity<Post>().HasData(posts);

            List<PostReaction> postReactions = new List<PostReaction>()
            {
                new PostReaction
                {
                    Id =1 ,
                    UserId = 1 ,
                    PostId = 1 ,
                    Reaction = Reactions.Like

                },
                new PostReaction
                {
                    Id =2 ,
                    UserId = 1 ,
                    PostId = 2 ,
                    Reaction = Reactions.Dislike

                },
                 new PostReaction
                {
                    Id =3 ,
                    UserId = 2 ,
                    PostId = 3 ,
                    Reaction = Reactions.Like

                },
                 new PostReaction
                {
                    Id = 4 ,
                    UserId = 2 ,
                    PostId = 4 ,
                    Reaction = Reactions.Dislike

                }
            };
            modelBuilder.Entity<PostReaction>().HasData(postReactions);

            List<CommentReaction> commentReactions = new List<CommentReaction>()
            {
                new CommentReaction
{
                    Id = 1 ,
                    CommentId = 1,
                    UserId = 1 ,
                    Reaction = Reactions.Like

                },
                new CommentReaction
                {
                    Id = 2 ,
                    CommentId = 2,
                    UserId = 1 ,
                    Reaction = Reactions.Dislike

                },
                 new CommentReaction
                {
                    Id = 3 ,
                    CommentId = 3,
                    UserId = 2 ,
                    Reaction = Reactions.Like

                },
                 new CommentReaction
                {
                    Id = 4 ,
                    CommentId = 4,
                    UserId = 2 ,
                    Reaction = Reactions.Dislike

                }
            };
            modelBuilder.Entity<CommentReaction>().HasData(commentReactions);
        }
    }
}

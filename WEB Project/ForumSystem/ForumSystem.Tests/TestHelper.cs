using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests
{
    public class TestHelper
    {
        public static Comment TestComment
        {
            get
            {
                return new Comment
                {
                    Id = 1,
                    CommentContent = "This is a Test comment.",
                    PostId = 2,
                    UserId = 1
                };
            }
        }

        public static Comment TestUpdatedComment
        {
            get
            {
                return new Comment
                {
                    Id = 1,
                    CommentContent = "This is an Updated Test comment.",
                    PostId = 2,
                    UserId = 1
                };
            }
        }



        public static CommentDto TestCommentDto
        {
            get
            {
                return new CommentDto
                {
                    CommentId = 1,
                    CommentContent = "This is a Test comment.",
                    Author = "bruce123"
                };
            }
        }

        public static CommentDto TestUpdatedCommentDto
        {
            get
            {
                return new CommentDto
                {
                    CommentId = 1,
                    CommentContent = "This is an Updated Test comment.",
                    Author = "bruce123"
                };
            }
        }


        public static List<Comment> TestListComments
        {
            get
            {
                return new List<Comment>()
                {
                    new Comment
                    {
                        Id = 1,
                        CommentContent = "This is first Test comment.",
                        PostId = 2,
                        UserId = 1
                    },
                    new Comment
                    {
                        Id = 2,
                        CommentContent = "This is second Test comment.",
                        PostId = 1,
                        UserId = 2
                    },
                    new Comment
                    {
                        Id = 3,
                        CommentContent = "This is third Test comment.",
                        PostId = 2,
                        UserId = 3
                    },
                    new Comment
                    {
                        Id = 4,
                        CommentContent = "This is forth Test comment.",
                        PostId = 1,
                        UserId = 1
                    }
                };
            }
        }

        public static List<CommentDto> TestListCommentDtos
        {
            get
            {
                return new List<CommentDto>()
                {
                    new CommentDto
                    {
                          CommentId = 1,
                          CommentContent = "This is first Test comment.",
                          Author = "bruce123"
                    },
                    new CommentDto
                    {
                          CommentId = 2,
                          CommentContent = "This is second Test comment.",
                          Author = "tony111"
                    },
                    new CommentDto
                    {
                          CommentId = 3,
                          CommentContent = "This is third Test comment.",
                          Author = "gogo3"
                    },
                    new CommentDto
                    {
                          CommentId = 4,
                          CommentContent = "This is forth Test comment.",
                          Author = "bruce123"
                    }
                };
            }
        }

        public static User TestUser
        {
            get
            {
                return new User
                {
                    Id = 1
                };
            }
        }

        public static User TestUser2
        {
            get
            {
                return new User
                {
                    Id = 2
                };
            }
        }

        public static Role TestRole
        {
            get
            {
                return new Role
                {
                    Id = 2,
                    Name = "Default"
                };
            }
        }

        public static Role TestAdmin
        {
            get
            {
                return new Role
                {
                    Id = 1,
                    Name = "Admin"
                };
            }
        }

        public static CommentReaction TestCommentReaction
        {
            get
            {
                return new CommentReaction
                {
                    Id = 1,
                    CommentId = 1,
                    UserId = 1,
                    Reaction = Reactions.Like
                };
            }
        }

        public static CommentReaction TestUpdatedCommentReaction
        {
            get
            {
                return new CommentReaction
                {
                    Id = 1,
                    CommentId = 1,
                    UserId = 1,
                    Reaction = Reactions.Dislike
                };
            }
        }

        public static CommentReactionDto TestCommentReacitonDto
        {
            get
            {
                return new CommentReactionDto
                {
                    Id = 1,
                    Author = "bruce123",
                    Reaction = "Like"
                };
            }
        }

        public static CommentReactionDto TestUpdatedCommentReacitonDto
        {
            get
            {
                return new CommentReactionDto
                {
                    Id = 1,
                    Author = "bruce123",
                    Reaction = "Dislike"
                };
            }
        }

        public static List<CommentReaction> TestListCommentReactions
        {
            get
            {
                return new List<CommentReaction>()
                {
                    new CommentReaction
                    {
                       Id = 1,
                       CommentId = 1,
                       UserId = 1,
                       Reaction = Reactions.Dislike
                    },
                    new CommentReaction
                    {
                       Id = 2,
                       CommentId = 1,
                       UserId = 2,
                       Reaction = Reactions.Like
                    }
                };
            }
        }

        public static PhoneNumber TestPhoneNumber
        {
            get
            {
                return new PhoneNumber
                {
                    Id = 1,
                    Number = "1111",
                    UserId = 1
                };
            }
        }

        public static PhoneNumber TestUpdatedPhoneNumber
        {
            get
            {
                return new PhoneNumber
                {
                    Id = 1,
                    Number = "2222",
                    UserId = 1
                };
            }
        }
        public static PhoneNumberDto TestPhoneNumberDto
        {
            get
            {
                return new PhoneNumberDto
                {
                    Id = 1,
                    Number = "1111",
                    Username = "gogo3"
                };
            }
        }

        public static PhoneNumberDto TestUpdatedPhoneNumberDto
        {
            get
            {
                return new PhoneNumberDto
                {
                    Id = 1,
                    Number = "2222",
                    Username = "gogo3"
                };
            }
        }

        public static List<PhoneNumber> TestListPhoneNumber
        {
            get
            {
                return new List<PhoneNumber>()
                {
                    new PhoneNumber
                    {
                        Id = 1,
                        Number = "2222",
                        UserId = 1
                    }
                };
            }
        }

        public static List<PhoneNumberDto> TestListPhoneNumberDto
        {
            get
            {
                return new List<PhoneNumberDto>()
                {
                    new PhoneNumberDto
                    {
                    Id = 1,
                    Number = "2222",
                    Username = "gogo3"
                    }
                };
            }
        }

    }
}


using ForumSystem.Models.DTO;
using ForumSystem.Models;
using ForumSystem.Models.Users.UsersViewModels;

namespace ForumSystem.Helpers.Contracts
{
    public interface IModelMapper
    {
        public Comment ToModel(CommentDto commentModel);
        public Comment ToModel(CommentDto commentModel, int postId, User user);
        public User MapUserCreate(CreateUserDto dto);
        UpdateUserDto MapUserEditView(EditViewModel editViewModel, string uniqueFileName);
        public CreateUserDto MapUserRegView(RegisterViewModel regViewModel);
        public User MapUserUpdate(int id, UpdateUserDto dto, User authUser);
        public PostDto MapPost(Post post);
        public Post MapPostCreate(PostDto dto);
        public Post MapPostCreate(PostDto dto, int userId);
        public PostReaction MapPostReactionCreate(int postId, User author, PostReactionDto dto);
        public CommentReaction ToModel(CommentReactionDto commentReactionDto, int commentId, User authUser);
        public PhoneNumber ToModel(PhoneNumberDto phoneNumberModel, int userId);
        public PhoneNumberDto ToDto(PhoneNumber phoneNumberModel);
        public CommentDto ToDto(Comment comment);
        public PostReactionDto MapPostReaction(PostReaction postReaction);
        public CommentReactionDto CommentReactionMap(CommentReaction commentReaction);
        public PostMVCDto ToMVCDto(PostDto post);


    }
}

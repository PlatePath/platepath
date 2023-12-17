using PlatePath.API.Data.Models.Forum;

namespace PlatePath.API.Services;

public interface IForumService
{
    List<Post> GetPosts(int page, int itemsPerPage);

    List<Comment> GetPostComments(int postId);

    List<Like> GetPostLikes(int postId);

    List<Like> GetCommentLikes(int commentId);

    bool LikePost(String userId, int postId);

    bool LikeComment(String userId, int commentId);

    bool CreatePost(Post post);

    bool EditPost(int postId, Post post);

    bool CreateComment(Comment comment);

    bool EditComment(int commentId, Comment comment);
}
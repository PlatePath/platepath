using PlatePath.API.Data.Models.Forum;

namespace PlatePath.API.Services;

public interface IForumService
{
    List<Post> GetPosts(int page, int itemsPerPage);

    List<Comment> GetPostComments(int postId);

    List<Like> GetPostLikes(int postId);

    List<Like> GetCommentLikes(int commentId);

    Like LikePost(string userId, int postId);

    Like LikeComment(string userId, int commentId);

    Post CreatePost(Post post);

    Post EditPost(int postId, Post post);

    Comment CreateComment(Comment comment);

    Comment EditComment(int commentId, Comment comment);
}
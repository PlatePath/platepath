using PlatePath.API.Data.Models.Forum;

namespace PlatePath.API.Services;

public class ForumService : IForumService
{
    public List<Post> GetPosts(int page, int itemsPerPage)
    {
        throw new NotImplementedException();
    }

    public List<Comment> GetPostComments(int postId)
    {
        throw new NotImplementedException();
    }

    public List<Like> GetPostLikes(int postId)
    {
        throw new NotImplementedException();
    }

    public List<Like> GetCommentLikes(int commentId)
    {
        throw new NotImplementedException();
    }

    public Like LikePost(string userId, int postId)
    {
        throw new NotImplementedException();
    }

    public Like LikeComment(string userId, int commentId)
    {
        throw new NotImplementedException();
    }

    public Post CreatePost(Post post)
    {
        throw new NotImplementedException();
    }

    public Post EditPost(int postId, Post post)
    {
        throw new NotImplementedException();
    }

    public Comment CreateComment(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Comment EditComment(int commentId, Comment comment)
    {
        throw new NotImplementedException();
    }
}
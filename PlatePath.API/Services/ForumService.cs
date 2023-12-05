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

    public bool LikePost(string userId, int postId)
    {
        throw new NotImplementedException();
    }

    public bool LikeComment(string userId, int commentId)
    {
        throw new NotImplementedException();
    }

    public bool CreatePost(Post post)
    {
        throw new NotImplementedException();
    }

    public bool EditPost(int postId, Post post)
    {
        throw new NotImplementedException();
    }

    public bool CreateComment(Comment comment)
    {
        throw new NotImplementedException();
    }

    public bool EditComment(int commentId, Comment comment)
    {
        throw new NotImplementedException();
    }
}
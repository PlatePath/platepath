using PlatePath.API.Data;
using PlatePath.API.Data.Models.Forum;

namespace PlatePath.API.Services;

public class ForumService : IForumService
{
    private readonly ApplicationDbContext dbContext;

    public ForumService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Post> GetPosts(int page, int itemsPerPage)
    {
        throw new NotImplementedException();
    }

    public List<Comment> GetPostComments(int postId)
    {
        List<Comment> comments = new List<Comment>();
        var post = dbContext.Posts.Find(postId);

        if (post == null)
        {
            return comments;
        }

        foreach (var comment in post.Comments)
        {
            comments.Add(comment);
        }

        return comments;
    }

    public List<Like> GetPostLikes(int postId)
    {
        var post = dbContext.Posts.Find(postId);
        List<Like> likes = new List<Like>();

        if (post == null)
        {
            return likes;
        }

        foreach (var like in post.Likes)
        {
            likes.Add(like);
        }

        return likes;
    }

    public List<Like> GetCommentLikes(int commentId)
    {
        var comment = dbContext.Comments.Find(commentId);
        List<Like> likes = new List<Like>();

        if (comment == null)
        {
            return likes;
        }

        foreach (var like in comment.Likes)
        {
            likes.Add(like);
        }

        return likes;
    }

    public bool LikePost(string userId, int postId)
    {
        var user = dbContext.Users.Find(userId);
        var post = dbContext.Posts.Find(postId);

        if (user == null || post == null)
        {
            return false;
        }

        var like = new Like()
        {
            Post = post,
            User = user,
        };

        user.Likes.Add(like);
        post.Likes.Add(like);

        dbContext.Likes.Add(like);
        dbContext.Posts.Update(post);
        dbContext.Users.Update(user);
        dbContext.SaveChanges();

        return true;
    }

    public bool LikeComment(string userId, int commentId)
    {
        var user = dbContext.Users.Find(userId);
        var comment = dbContext.Comments.Find(commentId);

        if (user == null || comment == null)
        {
            return false;
        }

        var like = new Like()
        {
            Comment = comment,
            User = user,
        };

        user.Likes.Add(like);
        comment.Likes.Add(like);

        dbContext.Likes.Add(like);
        dbContext.Comments.Update(comment);
        dbContext.Users.Update(user);
        dbContext.SaveChanges();

        return true;
    }

    public bool CreatePost(Post post)
    {
        dbContext.Posts.Add(post);
        dbContext.SaveChanges();

        return true;
    }

    public bool EditPost(int postId, Post post)
    {
        var postFromDb = dbContext.Posts.Find(postId);

        if (postFromDb == null)
        {
            return false;
        }

        postFromDb.Description = post.Description;
        postFromDb.Title = post.Title;

        dbContext.Posts.Update(postFromDb);
        dbContext.SaveChanges();

        return true;
    }

    public bool CreateComment(Comment comment)
    {
        dbContext.Comments.Add(comment);
        dbContext.SaveChanges();

        return true;
    }

    public bool EditComment(int commentId, Comment comment)
    {
        var commentFromDb = dbContext.Comments.Find(commentId);

        if (commentFromDb == null)
        {
            return false;
        }

        commentFromDb.Text = comment.Text;

        dbContext.Comments.Update(commentFromDb);
        dbContext.SaveChanges();

        return true;
    }
}
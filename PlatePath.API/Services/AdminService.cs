using PlatePath.API.Data;

namespace PlatePath.API.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext dbContext;

    public AdminService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public bool BanUser(string userId)
    {
        var user = dbContext.Users.Find(userId);
        if (user == null)
        {
            return false;
        }
        user.IsBanned = true;
        dbContext.Users.Update(user);
        dbContext.SaveChanges();
        return true;
    }

    public bool UnbanUser(string userId)
    {
        var user = dbContext.Users.Find(userId);
        if (user == null)
        {
            return false;
        }
        user.IsBanned = false;
        dbContext.Users.Update(user);
        dbContext.SaveChanges();
        return true;
    }

    public bool DeleteForumPost(int postId)
    {
        var post = dbContext.Posts.Find(postId);
        if (post == null)
        {
            return false;
        }
        dbContext.Posts.Remove(post);
        dbContext.SaveChanges();
        return true;
    }

    public bool DeleteComment(int commentId)
    {
        var comment = dbContext.Comments.Find(commentId);
        if (comment == null)
        {
            return false;
        }
        dbContext.Comments.Remove(comment);
        dbContext.SaveChanges();
        return true;
    }
}
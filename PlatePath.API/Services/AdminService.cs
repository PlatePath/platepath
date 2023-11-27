using PlatePath.API.Data;
using PlatePath.API.Data.Models.Forum;
using PlatePath.API.Data.Models.Users;

namespace PlatePath.API.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext dbContext;

    public AdminService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public void BanUser(int userId)
    {
        User? user = dbContext.Users.Find(userId);
        if (user == null)
        {
            // logic for user not found
            return;
        }
        user.IsBanned = true;
        dbContext.Users.Update(user);
        dbContext.SaveChanges();
    }

    public void DeleteForumPost(int postId)
    {
        Post? post = dbContext.Posts.Find(postId);
        if (post == null)
        {
            // logic for post not found
            return;
        }
        dbContext.Posts.Remove(post);
        dbContext.SaveChanges();
    }

    public void DeleteComment(int commentId)
    {
        Comment? comment = dbContext.Comments.Find(commentId);
        if (comment == null)
        {
            // logic for comment not found
            return;
        }
        dbContext.Comments.Remove(comment);
        dbContext.SaveChanges();
    }
}
using PlatePath.API.Data;
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
        }
        // ban the user and maybe return it?
    }

    public void DeleteForumPost(int postId)
    {
        throw new NotImplementedException();
    }

    public void DeletePostComment(int postId, int commentId)
    {
        throw new NotImplementedException();
    }
}
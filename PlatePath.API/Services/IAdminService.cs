namespace PlatePath.API.Services;

public interface IAdminService
{
    void BanUser(int userId);

    void DeleteForumPost(int postId);

    void DeleteComment(int commentId);
}
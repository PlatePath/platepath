namespace PlatePath.API.Services;

/// <summary>
/// Contains admin methods for banning users and deleting forum posts and comments.
/// </summary>
public interface IAdminService
{
    /// <summary>
    /// Bans the user with the given userId, setting its IsBanned state to true.
    /// </summary>
    /// <param name="userId">the id of the user.</param>
    /// <returns>true if the user was successfully banned, false if the user was not found.</returns>
    bool BanUser(string userId);

    /// <summary>
    /// Unbans the user with the given userId, setting its IsBanned state to false.
    /// </summary>
    /// <param name="userId">the id of the user.</param>
    /// <returns>true if the user was successfully unbanned, false if the user was not found.</returns>
    bool UnbanUser(string userId);

    /// <summary>
    /// Deletes the forum post with the given id.
    /// </summary>
    /// <param name="postId">the id of the post to be deleted.</param>
    /// <returns>true if the post was successfully deleted, false if the post was not found.</returns>
    bool DeleteForumPost(int postId);

    /// <summary>
    /// Deletes the comment with the given id.
    /// </summary>
    /// <param name="commentId">the id of the comment to be deleted.</param>
    /// <returns>true if the comment was successfully deleted, false if the comment was not found.</returns>
    bool DeleteComment(int commentId);
}
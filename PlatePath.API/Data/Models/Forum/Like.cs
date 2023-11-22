namespace PlatePath.API.Data.Models.Forum;

public class Like
{
    public int LikeId { get; set; }
    
    public int UserId { get; set; }
    
    public int PostId { get; set; }
    
    public int CommentId { get; set; }
}
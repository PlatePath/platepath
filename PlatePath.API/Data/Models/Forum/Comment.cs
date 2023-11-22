using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Forum;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int PostId { get; set; }
    
    public int UserId { get; set; }
    
    public int ParentCommentId { get; set; }

    public List<Comment> ChildComments { get; set; } = new();

    public List<Like> Likes { get; set; } = new();
    
    public int NumberOfLikes { get; set; }
    
    public string? Text { get; set; }
}
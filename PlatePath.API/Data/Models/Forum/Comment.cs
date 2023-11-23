using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlatePath.API.Data.Models.Users;

namespace PlatePath.API.Data.Models.Forum;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public Post Post { get; set; }
    
    public User User { get; set; }
    
    public int ParentCommentId { get; set; }

    public List<Comment> ChildComments { get; set; } = new();
    
    public List<Like> Likes { get; set; } = new();

    public int NumberOfLikes => Likes.Count();
    
    public string Text { get; set; }
}
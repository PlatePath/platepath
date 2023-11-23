using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Forum;

public class Like
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int PostId { get; set; }
    
    public int CommentId { get; set; }
}
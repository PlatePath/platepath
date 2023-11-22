using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Forum;

public class Reply
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int CommentId { get; set; }
    
    public string? Text { get; set; }
    
    // link this to the UserId that made the reply
}
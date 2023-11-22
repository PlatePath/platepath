using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Forum;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int PostId { get; set; }
    
    public string? Text { get; set; }

    public List<Reply> Replies { get; set; } = new();
    
    // link this to the UserId that made the comment
}
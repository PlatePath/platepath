using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Forum;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public int NumberOfLikes { get; set; }

    public List<Comment> Comments { get; set; } = new();

    /*public Recipe Recipe { get; set; }*/ // connect this to a recipe

    // link this to the UserId that made the post
}
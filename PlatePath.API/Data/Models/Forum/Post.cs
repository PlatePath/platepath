using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlatePath.API.Data.Models.Recipes;

namespace PlatePath.API.Data.Models.Forum;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int RecipeId { get; set; }
    
    public List<Comment> Comments { get; set; } = new();

    public List<Like> Likes { get; set; } = new();

    public int NumberOfLikes => Likes.Count();
    
    public string? Title { get; set; }

    public string? Description { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Data.Models.Users;

namespace PlatePath.API.Data.Models.Forum;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public User User { get; set; }
    
    public Recipe Recipe { get; set; }
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public ICollection<Like> Likes { get; set; } = new List<Like>();

    public int NumberOfLikes => Likes.Count();
    
    public string? Title { get; set; }

    public string? Description { get; set; }
}
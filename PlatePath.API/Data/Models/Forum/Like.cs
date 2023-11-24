using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlatePath.API.Data.Models.Users;

namespace PlatePath.API.Data.Models.Forum;

public class Like
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public User User { get; set; }
    
    public Post Post { get; set; }
    
    public Comment Comment { get; set; }
}
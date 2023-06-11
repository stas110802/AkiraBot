using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("User")]
public class User : IdentifierRealize
{
    public User() { }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegisterDate { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Entities;

/*
 * Users are also admins, the distinction should be made by the application layer 
 */
public class User
{
    public int userId { get; set; }
    [Required, MaxLength(64)]
    public string userName { get; set; }
    [Required, MaxLength(128)]
    public string password { get; set; }
    [Required, MaxLength(64)]
    public string firstName { get; set; }
    [Required, MaxLength(64)]
    public string lastName { get; set; }
    [Required, MaxLength(128)]
    public string email { get; set; }
    public int SecurityLevel { get; set; }

    
    public User() { }

    public User(int userId, string username,string password, string firstName, string lastName, int securityLevel)
    {
        this.userId = userId;
        this.userName = userName;
        this.firstName = firstName;
        this.lastName = lastName;
        this.password = password;
        this.SecurityLevel = securityLevel;
    }
    public User( string userName, string firstName, string lastName,string email, string password, int securityLevel)
    {
        this.userName = userName;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        SecurityLevel = securityLevel;
    }
}
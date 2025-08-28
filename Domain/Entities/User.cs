namespace Minimal01.Domain.Entities;

public class User
{
    public User(string email, string password)
    {
        this.Email = email;
        this.Password = password;
        this.Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
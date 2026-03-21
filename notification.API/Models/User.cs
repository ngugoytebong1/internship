namespace notification.API.Models;

public class User
{
    public int Id { get; set; }
    public string EmailAddress { get; set; } = null!; // The crucial channel ID
    public string FullName { get; set; } = null!;
    // We'll keep it simple for now, assuming we get the recipient's email from this.
}

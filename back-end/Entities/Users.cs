namespace backend.Models;

public class User
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int Age { get; set; }
}
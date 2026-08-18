namespace MyApp.DTOs;

public class UserDto
{
    public Guid UuId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Age { get; set; }
}
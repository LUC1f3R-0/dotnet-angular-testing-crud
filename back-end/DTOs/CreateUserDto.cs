using System.ComponentModel.DataAnnotations;

namespace MyApp.DTOs;

public class CreateUserDto
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Range(18, 99)]
    public int Age { get; set; }
}
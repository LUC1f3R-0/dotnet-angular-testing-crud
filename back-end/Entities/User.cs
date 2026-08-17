namespace backend.Models;

public class User
{
    public long id { get; set; }

    public Guid uuid { get; set; }

    public string firstName { get; set; } = string.Empty;

    public string lastName { get; set; } = string.Empty;

    public string email { get; set; } = string.Empty;

    public int age { get; set; }
}
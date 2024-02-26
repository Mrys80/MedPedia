namespace MedPedia.Entities.Dto;

public class CurrentUserDto
{
    public string Role { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string SessionId { get; set; }
}
namespace CircleCI.Entities.DbSet;

public class Saved
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    public Post Post { get; set; } = null!;
    public User User { get; set; } = null!;
}
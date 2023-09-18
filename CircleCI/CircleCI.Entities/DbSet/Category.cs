namespace CircleCI.Entities.DbSet;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
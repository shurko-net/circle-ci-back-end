namespace CircleCI.Entities.DbSet;

public class Category
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public CategoryList CategoryList { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
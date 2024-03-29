using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCI.Entities.DbSet;

public class CategoryList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
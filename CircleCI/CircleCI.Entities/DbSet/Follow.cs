namespace CircleCI.Entities.DbSet;

public class Follow
{
    public int Id { get; set; }
    public int FollowerUserId { get; set; }
    public int FollowedUserId { get; set; }
    
    public User FollowerUser { get; set; } = null!;
    public User FollowedUser { get; set; } = null!;
}
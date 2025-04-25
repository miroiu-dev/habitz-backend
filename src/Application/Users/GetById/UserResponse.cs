using Domain.Users;

namespace Application.Users.GetById;

public sealed record UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public ActivityLevel ActivityLevel { get; set; }
    public Gender Gender { get; set; }
    public Goal Goal { get; set; }
    public int? GoalWeight { get; set; }
    public int Height { get; set; }
    public decimal? WeeklyGoal{ get; set; }
    public int Weight { get; set; }
}

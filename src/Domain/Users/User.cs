using Domain.BodyMeasurementLogs;
using Domain.Habits;
using SharedKernel;
using System;
using System.Collections.Generic;

namespace Domain.Users;

public sealed partial class User
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
    public decimal? WeeklyGoal { get; set; }  
    public int Weight { get; set; } 
    public string PasswordHash { get; set; }
    public List<Habit> Habits { get; set; } = [];
    public List<BodyMeasurementLog> BodyMeasurementLogs { get; set; } = [];

    /// <summary>
    /// Calculates the Basal Metabolic Rate (BMR) using the Mifflin-St Jeor equation.
    /// </summary>
    public double CalculateBMR() =>
        Gender == Gender.Male
            ? 10 * Weight + 6.25 * Height - 5 * Age + 5
            : 10 * Weight + 6.25 * Height - 5 * Age - 161;

    /// <summary>
    /// Returns the activity multiplier based on user's ActivityLevel.
    /// </summary>
    private double GetActivityMultiplier() => ActivityLevel switch
    {
        ActivityLevel.Sedentary => 1.2,
        ActivityLevel.Light => 1.375,
        ActivityLevel.Active => 1.55,
        ActivityLevel.VeryActive => 1.725,
        _ => 1.2
    };

    /// <summary>
    /// Calculates Total Daily Energy Expenditure (TDEE).
    /// </summary>
    public double CalculateTDEE() => CalculateBMR() * GetActivityMultiplier();

    /// <summary>
    /// Returns the goal calorie adjustment based on weekly goal.
    /// 1kg of body fat ≈ 7700 calories, so adjust daily calories accordingly.
    /// </summary>
    private double GetGoalAdjustment()
    {
        if (!WeeklyGoal.HasValue || WeeklyGoal == 0)
        {
            return 0;
        }

        // Convert decimal WeeklyGoal to double for calculations
        double weeklyGoalKg = (double)WeeklyGoal.Value;

        // Calculate daily calorie adjustment (7700 calories per kg)
        double dailyAdjustment = Math.Abs(weeklyGoalKg) * 7700 / 7;

        return Goal switch
        {
            Goal.Lose => -dailyAdjustment,
            Goal.Gain => dailyAdjustment,
            Goal.Maintain => 0,
            _ => 0
        };
    }

    /// <summary>
    /// Calculates time to reach goal weight based on current pace.
    /// </summary>
    public TimeSpan? CalculateTimeToGoal()
    {
        if (!GoalWeight.HasValue || !WeeklyGoal.HasValue || WeeklyGoal == 0)
        {
            return null;
        }

        int weightDifference = GoalWeight.Value - Weight;

        // If there's no difference or goal doesn't align with direction
        if (weightDifference == 0 ||
            Goal == Goal.Lose && weightDifference >= 0 ||
            Goal == Goal.Gain && weightDifference <= 0)
        {
            return null;
        }

        // Calculate weeks needed (take absolute values to ensure positive result)
        double weeksNeeded = Math.Abs(weightDifference) / Math.Abs((double)WeeklyGoal.Value);
        return TimeSpan.FromDays(weeksNeeded * 7);
    }

    /// <summary>
    /// Returns the final calorie goal including activity and weight goal.
    /// </summary>
    public double GetDailyCalorieGoal() => Math.Max(1200, CalculateTDEE() + GetGoalAdjustment());

    /// <summary>
    /// Returns daily macronutrient targets in grams based on calorie goal.
    /// </summary>
    public Macros GetMacronutrientTargets()
    {
        double calorieGoal = GetDailyCalorieGoal();

        // Macro distribution based on goal
        (double proteinRatio, double fatRatio, double carbRatio) = Goal switch
        {
            Goal.Lose => (0.40, 0.30, 0.30),
            Goal.Gain => (0.25, 0.25, 0.50),
            _ => (0.30, 0.30, 0.40)  // Maintenance
        };

        // Protein: 4 calories per gram
        // Fat: 9 calories per gram
        // Carbs: 4 calories per gram
        double proteinGrams = Math.Round(calorieGoal * proteinRatio / 4);
        double fatGrams = Math.Round(calorieGoal * fatRatio / 9);
        double carbGrams = Math.Round(calorieGoal * carbRatio / 4);

        return new Macros
        {
            ProteinGrams = proteinGrams,
            FatGrams = fatGrams,
            CarbGrams = carbGrams,
            TotalCalories = calorieGoal
        };
    }

    /// <summary>
    /// Returns a breakdown of calorie calculations including BMR, TDEE, and estimated exercise calories.
    /// </summary>
    public CalorieBreakdown GetCalorieBreakdown()
    {
        double bmr = CalculateBMR();
        double tdee = CalculateTDEE();
        double activityCalories = tdee - bmr;
        double goalCalories = GetDailyCalorieGoal();

        return new CalorieBreakdown
        {
            BMR = Math.Round(bmr),
            ActivityCalories = Math.Round(activityCalories),
            GoalCalories = Math.Round(goalCalories)
        };
    }
}


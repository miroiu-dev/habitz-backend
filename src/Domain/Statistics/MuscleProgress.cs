using System;

namespace Domain.Statistics;

public class MuscleProgress
{
    public decimal InitialMeasurement { get; set; }
    public decimal CurrentMeasurement { get; set; }
    public decimal AbsoluteChange { get; set; }
    public decimal PercentageChange { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime CurrentDate { get; set; }

    public MuscleProgress(decimal initialMeasurement, decimal currentMeasurement, decimal absoluteChange, decimal percentageChange, DateTime initialDate, DateTime currentDate)
    {
        InitialMeasurement = initialMeasurement;
        CurrentMeasurement = currentMeasurement;
        AbsoluteChange = absoluteChange;
        PercentageChange = percentageChange;
        InitialDate = initialDate;
        CurrentDate = currentDate;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users;
public class Macros
{
    public double ProteinGrams { get; set; }
    public double FatGrams { get; set; }
    public double CarbGrams { get; set; }
    public double TotalCalories { get; set; }

    public double TotalGrams => ProteinGrams + FatGrams + CarbGrams;

    public double ProteinPercentage => Math.Round(ProteinGrams * 4 / TotalCalories * 100);
    public double FatPercentage => Math.Round(FatGrams * 9 / TotalCalories * 100);
    public double CarbPercentage => Math.Round(CarbGrams * 4 / TotalCalories * 100);
}

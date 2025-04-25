using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Users;

namespace Domain.BodyMeasurementLogs;
public class BodyMeasurementLog
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Neck { get; set; }
    public decimal LeftBiceps { get; set; }
    public decimal RightBiceps { get; set; }
    public decimal Chest { get; set; }
    public decimal Abs { get; set; }
    public decimal LeftTigh { get; set; }
    public decimal RightTigh { get; set; }
    public decimal LeftCalf { get; set; }
    public decimal RightCalf { get; set; }
    public decimal Shoulder { get; set; }
    public decimal Waist { get; set; }
    public decimal Hip { get; set; }
    public int UserId { get; set; }
    public decimal WaistHipRatio { get; set; }
    public User User {  get; set; }
}

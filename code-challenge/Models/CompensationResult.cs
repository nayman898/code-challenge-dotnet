using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
  public class CompensationResult
  {
    public Employee Employee { get; set; }
    public int Salary { get; set; }
    public DateTime EffectiveDate { get; set; }
  }
}

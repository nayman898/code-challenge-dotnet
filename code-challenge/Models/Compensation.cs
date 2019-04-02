using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
  public class Compensation
  {
    public String ID { get; set; }
    public Employee Employee { get; set; }
    public int Salary { get; set; }
    public DateTime EffectiveDate { get; set; }
  }
}

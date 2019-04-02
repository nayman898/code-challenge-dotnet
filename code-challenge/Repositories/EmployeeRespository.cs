using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
  public class EmployeeRespository : IEmployeeRepository
  {
    private readonly EmployeeContext _employeeContext;
    private readonly CompensationContext _compensationContext;
    private readonly ILogger<IEmployeeRepository> _logger;

    public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext, CompensationContext compensationContext)
    {
      _employeeContext = employeeContext;
      _logger = logger;
      _compensationContext = compensationContext;
    }

    public Employee Add(Employee employee)
    {
      employee.EmployeeId = Guid.NewGuid().ToString();
      _employeeContext.Employees.Add(employee);
      return employee;
    }

    public Employee GetById(string id)
    {
      return _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
    }

    public Task SaveAsync()
    {
      return _employeeContext.SaveChangesAsync();
    }

    public Employee Remove(Employee employee)
    {
      return _employeeContext.Remove(employee).Entity;
    }
    public Compensation GetCompensationByEmployeeId(string id)
    {
      return _compensationContext.Compensation.Include(e => e.Employee).SingleOrDefault(e => e.Employee.EmployeeId == id);
    }
    public Compensation AddCompensation(Compensation compensation)
    {
      compensation.ID = Guid.NewGuid().ToString();
      _compensationContext.Compensation.Add(compensation);
      _compensationContext.SaveChangesAsync();
      return compensation;
    }
  }
}

using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace challenge.Controllers
{
  [Route("api/Compensation")]
  public class CompensationController : Controller
  {
    private readonly ILogger _logger;
    private readonly IEmployeeService _employeeService;

    public CompensationController(ILogger<CompensationController> logger, IEmployeeService employeeService)
    {
      _logger = logger;
      _employeeService = employeeService;
    }
    [HttpPost]
    public IActionResult CreateCompensation([FromBody]Compensation compensation)
    {
      compensation.Employee = GetEmployeeById(compensation.Employee.EmployeeId);
      _logger.LogDebug($"Received Compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

      _employeeService.CreateCompensation(compensation);
      CompensationResult result = new CompensationResult();
      result.Employee = compensation.Employee;
      result.EffectiveDate = compensation.EffectiveDate;
      result.Salary = compensation.Salary;
      return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.Employee.EmployeeId }, result);
    }
    [HttpGet("employee/{id}", Name = "getCompensationByEmployeeId")]
    public IActionResult GetCompensationEmployeeById(String id)
    {
      _logger.LogDebug($"Received Compensation get request for '{id}'");

      var compensation = _employeeService.GetCompensationByEmployeeId(id);

      if (compensation == null)
        return NotFound();
      CompensationResult result = new CompensationResult();
      result.Employee = compensation.Employee;
      result.EffectiveDate = compensation.EffectiveDate;
      result.Salary = compensation.Salary;
      return Ok(result);
    }
    public Employee GetEmployeeById(String id)
    {
      _logger.LogDebug($"Received employee get request for '{id}'");

      var employee = _employeeService.GetById(id);

      if (employee == null)
        return null;

      return employee;
    }
  }
}

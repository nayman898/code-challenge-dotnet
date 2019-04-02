using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace challenge.Controllers
{
  [Route("api/reportingStructure")]
  public class ReportingController : Controller
  {
    private readonly IEmployeeService _employeeService;
    public ReportingController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
    {
      _employeeService = employeeService;
    }

    [HttpGet("{id}", Name = "reportingStructure")]
    public ReportingStructure reportingStructure(String id)
    {
      var employee = _employeeService.GetById(id);
      var totalReports = employee.DirectReports.Count;
      List<Employee> DirectReports = employee.DirectReports;
      foreach(Employee reportingEmployee in  DirectReports)
      {
        totalReports += ReportingEmployee(reportingEmployee); //calls method to calculate all directly reporting employees
      }
      ReportingStructure reportStructure = new ReportingStructure();
      reportStructure.employee = employee;
      reportStructure.numberOfReports = totalReports;
      return reportStructure;
    }

    /********
     I created a system that will automatically run through all of the employees that report directly to the immediate employee. This way
     if there is a continued stem that goes below 2 levels, they are still included in the count.
    **/
    public int ReportingEmployee(Employee reportingEmployee)
    {
      int totalReporting = 0;
      //I used this to get the accurate information of the employee. When you get the DirectReports from a nested employee, it does not house the next level employees DirectReports
      var employee = _employeeService.GetById(reportingEmployee.EmployeeId); 
      totalReporting += reportingEmployee.DirectReports.Count;
      if(employee.DirectReports.Count != 0)
      {
        foreach (Employee secondLevelEmployee in employee.DirectReports)
        {
          totalReporting += ReportingEmployee(secondLevelEmployee);
        }
      }
      return totalReporting;
    }
  }
}

using System;
using System.Collections.Generic;
using EmployeePaySlip.Web.Models;

namespace EmployeePaySlip.Web.Service
{
    public interface ICalculator
    {
        IEnumerable<PaySlip> CalculatePaySlips(IEnumerable<Employee> employees);
    }
}

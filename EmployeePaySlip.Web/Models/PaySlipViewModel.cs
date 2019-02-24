using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EmployeePaySlip.Web.Models
{
    [Serializable]
    public class PaySlipViewModel
    {
        public PaySlipViewModel()
        {
        }

        public IFormFile EmployeeSalaryFile { get; set; }
        public IEnumerable<EmployeeViewModel> Employees;
        public IEnumerable<PaySlipResultViewModel> PaySilps;

        public string ErrorMessage { get; set; }
        public string MessageLevel { get; set; }
    }
}

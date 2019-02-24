using System;
namespace EmployeePaySlip.Web.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
        }

        public EmployeeViewModel(Employee employee)
        {
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.AnnualSalary = employee.AnnualSalary;
            this.SuperRate = employee.SuperRate;
            this.PaymentStartDate = employee.PaymentStartDate;
        }

        public EmployeeViewModel(string FirstName, string LastName, decimal AnnualSalary, decimal SuperRate, string PaymentStartDate)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.AnnualSalary = AnnualSalary;
            this.SuperRate = SuperRate;
            this.PaymentStartDate = PaymentStartDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal SuperRate { get; set; }
        public string PaymentStartDate { get; set; }
    }
}

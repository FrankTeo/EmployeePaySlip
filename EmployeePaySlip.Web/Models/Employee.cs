using System;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace EmployeePaySlip.Web.Models
{
    public class Employee : IEmployee
    {
        public Employee()
        {
        }

        [Name("first_name")]
        public string FirstName { get; set; }
        [Name("last_name")]
        public string LastName { get; set; }
        [Name("annual_salary")]
        public decimal AnnualSalary { get; set; }
        [Name("super_rate")]
        public decimal SuperRate { get; set; }
        [Name("payment_start_date")]
        public string PaymentStartDate { get; set; }

        public Employee(EmployeeViewModel employeeViewModel)
        {
            FirstName = employeeViewModel.FirstName;
            LastName = employeeViewModel.LastName;
            AnnualSalary = employeeViewModel.AnnualSalary;
            SuperRate = employeeViewModel.SuperRate;
            PaymentStartDate = employeeViewModel.PaymentStartDate;
        }

        public Employee(string FirstName, string LastName, decimal AnnualSalary, decimal SuperRate, string PaymentStartDate)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.AnnualSalary = AnnualSalary;
            this.SuperRate = SuperRate;
            this.PaymentStartDate = PaymentStartDate;
        }

        public string FullName()
        {
            return FirstName + " " + LastName;

        }
    }
}

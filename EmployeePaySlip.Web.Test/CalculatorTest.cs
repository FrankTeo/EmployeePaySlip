using System;
using System.Collections.Generic;
using EmployeePaySlip.Web.Models;
using EmployeePaySlip.Web.Service;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Linq;

namespace EmployeePaySlip.Web.Test
{
    public class CalculatorTest : IDisposable
    {
        public Calculator calculator = null;
        public IEnumerable<Employee> employees;
        public List<PaySlip> paySlipsExpect;
        public CalculatorTest()
        {
            calculator = new Calculator(null);
            employees = new[]
            {
                new Employee("David", "Rudd", 60050m, 0.09m, "01 March - 31 March"),
                new Employee("Ryan", "Chen", 120000m, 0.10m, "01 March - 31 March")
            };

            paySlipsExpect = new[]
            {
                new PaySlip("David Rudd", "01 March - 31 March", 5004m, 922m, 4082m, 450m),
                new PaySlip("Ryan Chen", "01 March - 31 March", 10000m, 2669m, 7331m, 1000m)
            }.ToList();
        }

        [Fact(DisplayName = "CalculatePaySlips")]
        public void Test_CalculatePaySlips_Success()
        {
            List<PaySlip> paySlips = calculator.CalculatePaySlips(employees).ToList();

            for(int i = 0; i < paySlipsExpect.Count(); i++)
            {
                Assert.Equal(paySlipsExpect[i].Name, paySlips[i].Name);
                Assert.Equal(paySlipsExpect[i].GrossIncome, paySlips[i].GrossIncome);
                Assert.Equal(paySlipsExpect[i].IncomeTax, paySlips[i].IncomeTax);
                Assert.Equal(paySlipsExpect[i].NetIncome, paySlips[i].NetIncome);
                Assert.Equal(paySlipsExpect[i].PayPeriod, paySlips[i].PayPeriod);
                Assert.Equal(paySlipsExpect[i].Super, paySlips[i].Super);
            }
        }

        [Fact(DisplayName = "CalculateGrossIncome")]
        public void Test_CalculateGrossIncome_Success()
        {
            decimal grossIncome = calculator.CalculateGrossIncome(employees.ToList()[0].AnnualSalary);
            Assert.Equal(paySlipsExpect[0].GrossIncome, grossIncome);
        }

        [Fact(DisplayName = "CalculateIncomeTax")]
        public void Test_CalculateIncomeTax_Success()
        {
            decimal salary = employees.ToList()[0].AnnualSalary;
            TaxRate taxRate = calculator.MatchTaxRate(salary);
            decimal IncomeTax = calculator.CalculateIncomeTax(salary, taxRate);
            Assert.Equal(paySlipsExpect[0].IncomeTax, IncomeTax);
        }

        [Fact(DisplayName = "CalculateNetIncome")]
        public void Test_CalculateNetIncome_Success()
        {
            decimal salary = employees.ToList()[0].AnnualSalary;
            TaxRate taxRate = calculator.MatchTaxRate(salary);
            decimal NetIncome = calculator.CalculateNetIncome(salary, taxRate);
            Assert.Equal(paySlipsExpect[0].NetIncome, NetIncome);
        }

        [Fact(DisplayName = "CalculateSuper")]
        public void Test_CalculateSuper_Success()
        {
            decimal Super = calculator.CalculateSuper(employees.ToList()[0].AnnualSalary, employees.ToList()[0].SuperRate);
            Assert.Equal(paySlipsExpect[0].Super, Super);
        }

        public void Dispose()
        {

        }
    }
}

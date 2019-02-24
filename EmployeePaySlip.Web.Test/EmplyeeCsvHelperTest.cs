using System;
using System.Collections.Generic;
using EmployeePaySlip.Web.Models;
using EmployeePaySlip.Web.Service;
using Microsoft.Extensions.Logging;
using Xunit;
using System.IO;
using System.Linq;

namespace EmployeePaySlip.Web.Test
{
    public class EmplyeeCsvHelperTest : IDisposable
    {
        FileStream fileStreamSuccess, fileStreamError;
        List<Employee> employeesExcept;
        EmployeeCsvHelper employeeCsvHelper;
        public EmplyeeCsvHelperTest()
        {
            fileStreamSuccess = File.OpenRead(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"../../../TestFile/UnitTestFileSuccess.csv"));
            fileStreamError = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), @"../../../TestFile/ErrorFile.csv"));
            employeesExcept = new[]
            {
                new Employee("David", "Rudd", 60050m, 0.09m, "01 March - 31 March"),
                new Employee("Ryan", "Chen", 120000m, 0.10m, "01 March - 31 March")
            }.ToList();

            employeeCsvHelper = new EmployeeCsvHelper();
        }

        [Fact]
        public void Test_ReadFromCsvFile_Success()
        {
            List<Employee> employees = employeeCsvHelper.ReadFromCsvFile<Employee>(fileStreamSuccess).ToList();

            Assert.Equal(employeesExcept.Count(), employees.Count());
            for(int i = 0; i < employees.Count(); i++)
            {
                Assert.Equal(employees[i].AnnualSalary, employeesExcept[i].AnnualSalary);
                Assert.Equal(employees[i].FirstName, employeesExcept[i].FirstName);
                Assert.Equal(employees[i].LastName, employeesExcept[i].LastName);
                Assert.Equal(employees[i].PaymentStartDate, employeesExcept[i].PaymentStartDate);
                Assert.Equal(employees[i].SuperRate, employeesExcept[i].SuperRate);
            }
        }

        [Fact]
        public void Test_ReadFromCsvFile_Failure()
        {
            Type actual = null;
            try
            {
                employeeCsvHelper.ReadFromCsvFile<Employee>(fileStreamError);
            }
            catch (Exception e)
            {
                actual = e.GetType();
            }
            Assert.Equal(typeof(CsvHelper.MissingFieldException), actual);
        }

        public void Dispose()
        {

        }
    }
}

using System;
using CsvHelper.Configuration;
using EmployeePaySlip.Web.Models;

namespace EmployeePaySlip.Web.Service
{
    public class EmployeeCsvMapping : ClassMap<Employee>
    {

        public EmployeeCsvMapping()
        {

            Map(m => m.FirstName).Name("first_name");
            Map(m => m.LastName).Name("last_name");
            Map(m => m.AnnualSalary).Name("annual_salary");
            Map(m => m.SuperRate).Name("super_rate").ConvertUsing(r =>
            {
                var field = r.GetField("super_rate").Replace("%", "");
                if (decimal.TryParse(field, out decimal result))
                {
                    return result/100;
                }
                throw new InvalidOperationException();
            });
            Map(m => m.PaymentStartDate).Name("payment_start_date");

        }
    }
}

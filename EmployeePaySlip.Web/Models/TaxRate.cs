using System;
using System.Collections.Generic;

namespace EmployeePaySlip.Web.Models
{
    public class TaxRate : ITaxRate
    {
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal RateValue { get; set; }

        public static TaxRate Create(decimal MinSalary, decimal MaxSalary, decimal BaseAmount, decimal RateValue)
        {
            TaxRate taxRate = new TaxRate();
            taxRate.MinSalary = MinSalary;
            taxRate.MaxSalary = MaxSalary;
            taxRate.BaseAmount = BaseAmount;
            taxRate.RateValue = RateValue;
            return taxRate;
        }

    }
}

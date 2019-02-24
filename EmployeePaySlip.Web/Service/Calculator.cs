using System;
using System.Collections.Generic;
using EmployeePaySlip.Web.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
namespace EmployeePaySlip.Web.Service
{
    public class Calculator : ICalculator
    {
        private readonly IEnumerable<TaxRate> _taxRates;
        private readonly ILogger<ICalculator> _logger;
        public Calculator(
            ILogger<ICalculator> logger)
        {
            _logger = logger;
            _taxRates = TaxTable.TaxRates;
        }

        public TaxRate MatchTaxRate(decimal salary)
        {
            foreach(var tr in _taxRates)
            {
                if(tr.MinSalary <= salary && tr.MaxSalary >= salary)
                {
                    return tr;
                }
            }

            Exception e = new System.Exception("Tax Rate unmatch");
            _logger.LogError(e, $"");
            throw e;
        }

        public IEnumerable<PaySlip> CalculatePaySlips(IEnumerable<Employee> employees)
        {
            return (from e in employees.ToList()
                    let salary = e.AnnualSalary
                    let taxRate = MatchTaxRate(salary)
                    select new PaySlip
                    {
                        Name=e.FullName(),
                        PayPeriod = e.PaymentStartDate,
                        GrossIncome = CalculateGrossIncome(salary),
                        IncomeTax = CalculateIncomeTax(salary, taxRate),
                        NetIncome = CalculateNetIncome(salary, taxRate),
                        Super = CalculateSuper(salary, e.SuperRate)
                    }
                    ).ToList();
            
        }


        public decimal CalculateGrossIncome(decimal salary)
        {
            return decimal.Round(salary / 12, MidpointRounding.AwayFromZero);
        }

        public decimal CalculateIncomeTax(decimal salary, TaxRate taxRate)
        {
            return decimal.Round(
                (taxRate.BaseAmount + (salary - taxRate.MinSalary + 1) * taxRate.RateValue) / 12, MidpointRounding.AwayFromZero
            );
        }

        public decimal CalculateNetIncome(decimal salary, TaxRate taxRate)
        {
            return decimal.Round(
                CalculateGrossIncome(salary) - CalculateIncomeTax(salary, taxRate), MidpointRounding.AwayFromZero
            );
        }

        public decimal CalculateSuper(decimal salary, decimal SuperRate)
        {
            return decimal.Round(
                CalculateGrossIncome(salary) * SuperRate, MidpointRounding.AwayFromZero
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

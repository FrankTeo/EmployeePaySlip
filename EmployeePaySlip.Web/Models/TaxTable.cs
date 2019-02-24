using System;
using System.Collections.Generic;

namespace EmployeePaySlip.Web.Models
{
    public static class TaxTable
    {
        public static IEnumerable<TaxRate> TaxRates => new []
        {
            TaxRate.Create(0m, 18200m, 0m, 0m),
            TaxRate.Create(18201m, 37000m, 0m, 0.19m),
            TaxRate.Create(37001m, 87000m, 3572m, 0.325m),
            TaxRate.Create(87001m, 180000m, 19822m, 0.37m),
            TaxRate.Create(180001m, decimal.MaxValue, 54232m, 0.45m),
        };
    }
}

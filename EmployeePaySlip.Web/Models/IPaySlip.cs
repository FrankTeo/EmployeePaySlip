using System;
using System.Runtime.Serialization;

namespace EmployeePaySlip.Web.Models
{

    public interface IPaySlip 
    {
        string Name { get; set; }
        string PayPeriod { get; set; }
        decimal GrossIncome { get; set; }
        decimal IncomeTax { get; set; }
        decimal NetIncome { get; set; }
        decimal Super { get; set; }
    }


}

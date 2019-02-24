using System;
using System.Runtime.Serialization;

namespace EmployeePaySlip.Web.Models
{
    [Serializable]
    public class PaySlipResultViewModel :IPaySlip
    {
        public PaySlipResultViewModel()
        {
        
        }

        public PaySlipResultViewModel(string Name, string PayPeriod, decimal GrossIncome, decimal IncomeTax, decimal NetIncome, decimal Super)
        {
            this.Name = Name;
            this.PayPeriod = PayPeriod;
            this.GrossIncome = GrossIncome;
            this.IncomeTax = IncomeTax;
            this.NetIncome = NetIncome;
            this.Super = Super;
        }

        public PaySlipResultViewModel(PaySlip paySlip)
        {
            Name = paySlip.Name;
            PayPeriod = paySlip.PayPeriod;
            GrossIncome = paySlip.GrossIncome;
            IncomeTax = paySlip.IncomeTax;
            NetIncome = paySlip.NetIncome;
            Super = paySlip.Super;
        }

        public string Name { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }


    }
}

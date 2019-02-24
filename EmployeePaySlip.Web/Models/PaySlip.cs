using System;
using System.Runtime.Serialization;

namespace EmployeePaySlip.Web.Models
{
    [Serializable()]
    public class PaySlip : IPaySlip
    {
        public PaySlip()
        {
        }
        public PaySlip(string Name, string PayPeriod, decimal GrossIncome, decimal IncomeTax, decimal NetIncome, decimal Super)
        {
            this.Name = Name;
            this.PayPeriod = PayPeriod;
            this.GrossIncome = GrossIncome;
            this.IncomeTax = IncomeTax;
            this.NetIncome = NetIncome;
            this.Super = Super;
        }

        public PaySlip(PaySlipResultViewModel paySlipResultView)
        {
            this.Name = paySlipResultView.Name;
            this.PayPeriod = paySlipResultView.PayPeriod;
            this.GrossIncome = paySlipResultView.GrossIncome;
            this.IncomeTax = paySlipResultView.IncomeTax;
            this.NetIncome = paySlipResultView.NetIncome;
            this.Super = paySlipResultView.Super;
        }

        public bool PaySlipEquals(PaySlip obj)
        {
            if(this.Name == obj.Name
                && this.PayPeriod == obj.PayPeriod
                && this.GrossIncome == obj.GrossIncome
                && this.IncomeTax == obj.IncomeTax
                && this.NetIncome == obj.NetIncome
                && this.Super == obj.Super
                )
            {
                return true;
            }

            return false;
        }

        public string Name { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }

    }
}

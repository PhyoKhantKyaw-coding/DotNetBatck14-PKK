using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDatabase
{
    public class YearlyPaymentScheduleModel
    {
        public int Year { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal RemainingBalance { get; set; }
    }
    public class MonthlyPaymentScheduleModel
    {
        public int Month { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}

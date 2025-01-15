using DotNetBatch14PKK.LoanTrackerDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBatch14PKK.LoanTrackerDomain
{
    public class PaymentScheduleService
    {
        private readonly AppDbContext _dbContext;

        public PaymentScheduleService()
        {
            _dbContext = new AppDbContext();
        }

        public List<YearlyPaymentScheduleModel> GetYearlyPaymentSchedule(string loanId)
        {
            var loan = _dbContext.LoanDetails.FirstOrDefault(x => x.Id == loanId);
            if (loan == null)
                throw new Exception("Loan not found.");

            int year = loan.LoanTerm;
            decimal remainingBalance = loan.LoanAmount;
            var yearlySchedule = new List<YearlyPaymentScheduleModel>();
            for (int i = 0; i < year; i++)
            {
               var date = loan.StartDate.AddYears(i);
                decimal interest = remainingBalance * (loan.InterestRate / 100);
                decimal principal = loan.MonthlyPayment - interest;
                decimal totalPayment = principal+interest;
                remainingBalance = remainingBalance - principal;

                yearlySchedule.Add(new YearlyPaymentScheduleModel
                {
                    Year = date.Year,
                    Principal = principal,
                    Interest = interest,
                    TotalPayment = totalPayment,
                    RemainingBalance = remainingBalance
                });

            }     
            return yearlySchedule;
        }

        public List<MonthlyPaymentScheduleModel> GetMonthlyPaymentSchedule(string loanId)
        {
            var loan = _dbContext.LoanDetails.FirstOrDefault(x => x.Id == loanId );
            if (loan == null)
                throw new Exception("Loan not found.");
            decimal remainingBalance = loan.LoanAmount;
            var monthlySchedule = new List<MonthlyPaymentScheduleModel>();
            for (int i = 0; i < loan.LoanTerm*12; i++)
            {
                var date = loan.StartDate.AddMonths(i);
                decimal interest = remainingBalance * (loan.InterestRate / 100)/12;
                decimal principal = loan.MonthlyPayment - interest;
                decimal totalPayment = principal + interest;
                remainingBalance = remainingBalance - principal;
                monthlySchedule.Add(new MonthlyPaymentScheduleModel
                {
                    Month = date.Month,
                    Principal = principal,
                    Interest = interest,
                    TotalPayment = totalPayment,
                    RemainingBalance = remainingBalance
                });
            }
            return monthlySchedule;
        }
    }
}

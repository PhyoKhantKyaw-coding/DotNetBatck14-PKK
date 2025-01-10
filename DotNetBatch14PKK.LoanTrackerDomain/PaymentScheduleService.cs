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

            var payments = _dbContext.Payment.Where(x => x.LoanId == loanId).ToList();
            var groupedPayments = payments
                .GroupBy(p => p.PaymentDate.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    TotalPayment = g.Sum(p => p.AmountPaid),
                    TotalLateFees = g.Sum(p => p.LateFee)
                }).ToList();

            decimal remainingBalance = loan.LoanAmount;

            var yearlySchedule = new List<YearlyPaymentScheduleModel>();
            foreach (var group in groupedPayments)
            {
                decimal interest = remainingBalance * (loan.InterestRate / 100);
                decimal principal = group.TotalPayment - interest;
                remainingBalance -= group.TotalPayment;

                yearlySchedule.Add(new YearlyPaymentScheduleModel
                {
                    Year = group.Year,
                    Principal = principal,
                    Interest = interest,
                    TotalPayment = group.TotalPayment,
                    RemainingBalance =remainingBalance
                });
            }

            return yearlySchedule;
        }

        public List<MonthlyPaymentScheduleModel> GetMonthlyPaymentSchedule(string loanId, int year)
        {
            var loan = _dbContext.LoanDetails.FirstOrDefault(x => x.Id == loanId);
            if (loan == null)
                throw new Exception("Loan not found.");

            var payments = _dbContext.Payment
                .Where(x => x.LoanId == loanId && x.PaymentDate.Year == year)
                .ToList();

            decimal remainingBalance = loan.LoanAmount;
            var monthlySchedule = new List<MonthlyPaymentScheduleModel>();

            for (int i = 1; i <= 12; i++)
            {
                var monthlyPayments = payments.Where(x => x.PaymentDate.Month == i).ToList();
                decimal totalPayment = monthlyPayments.Sum(x => x.AmountPaid);
                decimal totalLateFees = monthlyPayments.Sum(x => x.LateFee);
                decimal interest = remainingBalance * (loan.InterestRate / 100) / 12;
                decimal principal = totalPayment - interest;
                remainingBalance -= totalPayment;

                monthlySchedule.Add(new MonthlyPaymentScheduleModel
                {
                    Month = i,
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    TotalPayment = Math.Round(totalPayment, 2),
                    RemainingBalance = Math.Round(remainingBalance, 2)
                });
            }

            return monthlySchedule;
        }
    }
}

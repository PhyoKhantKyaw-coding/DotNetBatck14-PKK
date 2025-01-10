using DotNetBatch14PKK.LoanTrackerDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDomain;

public class MortgageLoanService
{
    private readonly AppDbContext _dbContext;
    public MortgageLoanService()
    {
        _dbContext = new AppDbContext();
    }
    public ResponseModel CreateMortgageLoan(LoanDTO requestModel)
    {
        try
        {
            var customer = _dbContext.Customer.AsNoTracking().FirstOrDefault(x => x.Id == requestModel.CustomerId);
            if (customer is null)
            {
                return new ResponseModel { Message = "User not found!" };
            }

            var principle = requestModel.LoanAmount - requestModel.DownPayment;
            var monthlyInterestRate = (double)requestModel.InterestRate/100 / 12;
            var totalMonths = requestModel.LoanTerm * 12;
            var monthlyPayment = principle * (decimal)(monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths)) /
                                     (decimal)(Math.Pow(1 + monthlyInterestRate, totalMonths) - 1);
            var totalRepayment = principle + (principle * (requestModel.InterestRate / 100) *requestModel.LoanTerm);

            var model = new MortgageLoanModel
            {
                CustomerId = requestModel.CustomerId,
                LoanAmount = requestModel.LoanAmount,
                InterestRate = requestModel.InterestRate,
                LoanTerm = requestModel.LoanTerm,
                MonthlyPayment = monthlyPayment,
                DownPayment = requestModel.DownPayment,
                TotalRepayment = totalRepayment
            };

            _dbContext.LoanDetails.Add(model);
            var result = _dbContext.SaveChanges();

            string message = result > 0 ? "Successfully Create Loan." : "Fail to Create Loan!";
            ResponseModel response = new ResponseModel();
            response.IsSuccess = result > 0;
            response.Message = message;

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public List<MortgageLoanModel> GetMortgageLoans()
    {
        var loans = _dbContext.LoanDetails.ToList();
        return loans;
    }
    public ResponseModel GetMortgageLoanById(string id)
    {
        var loan = _dbContext.LoanDetails.FirstOrDefault(x => x.Id == id);
        if (loan == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Loan not found"
            };
        }
        return new ResponseModel
        {
            IsSuccess = true,
            Message = "Loan found",
            Data = loan
        };
    }
}

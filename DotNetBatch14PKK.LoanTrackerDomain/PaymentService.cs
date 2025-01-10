using DotNetBatch14PKK.LoanTrackerDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDomain;

public class PaymentService
{
    private readonly AppDbContext _dbContext;
    public PaymentService()
    {
        _dbContext = new AppDbContext();
    }
    public ResponseModel CreatePayment(PaymentModel payment, string loanid)
    {
        var loan = _dbContext.LoanDetails.FirstOrDefault(x => x.Id == loanid);
        if (loan == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Loan not found"
            };
        }  
        var paylst = _dbContext.Payment.Where(x => x.LoanId == loanid).ToList();
        var totalPaid = paylst.Sum(x => x.AmountPaid);
        var totalRepayment = loan.TotalRepayment;
        if (totalPaid + payment.AmountPaid > totalRepayment)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Amount paid exceeds total repayment"
            };
        }
        payment.Id = Guid.NewGuid().ToString();
        payment.LoanId = loan.Id;
        _dbContext.Payment.Add(payment);
        var result = _dbContext.SaveChanges();
        return new ResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Payment created successfully" : "Failed to create payment"
        };
    }
    public List<PaymentModel> GetPayments()
    {
        var payments = _dbContext.Payment.ToList();
        return payments;
    }
    public ResponseModel GetPaymentById(string id)
    {
        var payment = _dbContext.Payment.FirstOrDefault(x => x.Id == id);
        if (payment == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Payment not found"
            };
        }
        return new ResponseModel
        {
            IsSuccess = true,
            Message = "Payment found",
            Data = payment
        };
    }
}

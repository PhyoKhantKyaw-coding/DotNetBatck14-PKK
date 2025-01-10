using DotNetBatch14PKK.LoanTrackerDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDomain;


public class LateFeeRuleService
{
    private readonly AppDbContext _dbContext;
    public LateFeeRuleService()
    {
        _dbContext = new AppDbContext();
    }
    public ResponseModel CreateLateFeeRule(LateFeeRuleModel lateFeeRule)
    {
        lateFeeRule.Id = Guid.NewGuid().ToString();
        _dbContext.LateFee.Add(lateFeeRule);
        var result = _dbContext.SaveChanges();
        return new ResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Late Fee Rule created successfully" : "Failed to create Late Fee Rule"
        };
    }
    public List<LateFeeRuleModel> GetLateFeeRules()
    {
        var lateFeeRules = _dbContext.LateFee.ToList();
        return lateFeeRules;
    }
    public ResponseModel GetLateFeeRuleById(string id)
    {
        var lateFeeRule = _dbContext.LateFee.FirstOrDefault(x => x.Id == id);
        if (lateFeeRule == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Late Fee Rule not found"
            };
        }
        return new ResponseModel
        {
            IsSuccess = true,
            Message = "Late Fee Rule found",
            Data = lateFeeRule
        };
    }
    public ResponseModel UpdateLateFeeRule(string id, int MinDaysOverdue, int MaxDaysOverdue, decimal LateFeeAmount)
    {
        var lateFeeRule = _dbContext.LateFee.FirstOrDefault(x => x.Id == id);
        if (lateFeeRule == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Late Fee Rule not found"
            };
        }
        if (MinDaysOverdue > 0)
        {
            lateFeeRule.MinDaysOverdue = MinDaysOverdue;
        }
        if (MaxDaysOverdue > 0)
        {
            lateFeeRule.MaxDaysOverdue = MaxDaysOverdue;
        }
        if (LateFeeAmount > 0)
        {
            lateFeeRule.LateFeeAmount = LateFeeAmount;
        }
        _dbContext.Entry(lateFeeRule).State = EntityState.Modified;
        var result = _dbContext.SaveChanges();
        return new ResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Late Fee Rule updated successfully" : "Failed to update Late Fee Rule"
        };
    }
    public ResponseModel DeleteLateFeeRule(string id)
    {
        var lateFeeRule = _dbContext.LateFee.FirstOrDefault(x => x.Id == id);
        if (lateFeeRule == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Late Fee Rule not found"
            };
        }
        _dbContext.LateFee.Remove(lateFeeRule);
        var result = _dbContext.SaveChanges();
        return new ResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Late Fee Rule deleted successfully" : "Failed to delete Late Fee Rule"
        };
    }

}

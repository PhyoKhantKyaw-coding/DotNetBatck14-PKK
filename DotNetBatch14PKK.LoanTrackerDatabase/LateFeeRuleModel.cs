using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDatabase;
[Table("TBLLateFeeRule")]
public class LateFeeRuleModel
{
    [Key]
    public string Id { get; set; }
    public int MinDaysOverdue { get; set; }
    public int MaxDaysOverdue { get; set; }
    public decimal LateFeeAmount { get; set; }
}

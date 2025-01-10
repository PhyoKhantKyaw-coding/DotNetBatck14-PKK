using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDatabase;
[Table("TBLPayment")]
public class PaymentModel
{
    [Key]
    public string Id { get; set; }
    public string LoanId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal LateFee { get; set; }
}

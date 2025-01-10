using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DotNetBatch14PKK.LoanTrackerDatabase;
[Table("TBLCustomer")]
public class CustomerModel
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PKK.Login.Models
{
    [Table("TBL_Feature")]
    public class FeatureModel
    {
        [Key]
        public Guid FeatureID { get; set; }
        public string? FeatureName { get; set; }
    }
}

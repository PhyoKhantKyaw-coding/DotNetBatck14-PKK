using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DotNetBatch14PKK.Login.Models
{
    [Table("TBL_UserPermission")]
    public class UserPermissionModel
    {
        [Key]
        public Guid UserPermissionID { get; set; }
        public Guid RoleID { get; set; } 
        public Guid FeatureID { get; set; } 
    }
}

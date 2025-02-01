using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PKK.Login.Models
{
    [Table("TBL_Role")]
    public class RoleModel
    {
        [Key]
        public Guid RoleID { get; set; }
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PKK.Login.Models
{
    [Table("TBL_User")]
    public class UserModel
    {
        [Key]
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? RoleCode { get; set; } 
        public string? Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    [Table("TBL_User")]
    public class UserModel
    {
        [Key]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}

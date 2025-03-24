using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WMS_ERP_Backend.Models.AuthModels
{
    public class RegisterRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}

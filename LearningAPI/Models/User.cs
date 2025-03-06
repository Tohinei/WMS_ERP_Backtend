using System.ComponentModel.DataAnnotations;

namespace LearningAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; }

        [Range(18, 100)]
        public required int Age { get; set; }

        public Role Role { get; set; } = Role.Employee;
        public bool IsActive { get; set; } = false;

        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(50)]
        public required string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LearningAPI.Models
{
    public class RegisterRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

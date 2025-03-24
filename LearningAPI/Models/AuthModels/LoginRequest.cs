using System.ComponentModel.DataAnnotations;

namespace WMS_ERP_Backend.Models.AuthModels
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

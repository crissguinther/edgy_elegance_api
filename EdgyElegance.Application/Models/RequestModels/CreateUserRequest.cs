using System.ComponentModel.DataAnnotations;

namespace EdgyElegance.Application.Models.RequestModels {
    public class CreateUserRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
                
        [Compare(nameof(Password), ErrorMessage = "Password confirmation doesn't match")]
        public string? PasswordConfirmation { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EdgyElegance.Application.Models.RequestModels {
    public class LoginRequest {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

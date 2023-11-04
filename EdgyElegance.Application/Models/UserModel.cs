using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EdgyElegance.Application.Models {
    public class UserModel {
        public string? Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? Password { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        [Compare(nameof(Password), ErrorMessage = "Password confirmation doesn't match")]
        public string? PasswordConfirmation { get; set; }
    }
}

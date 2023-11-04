using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdgyElegance.Identity.Entities {
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser {
        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
    }
}

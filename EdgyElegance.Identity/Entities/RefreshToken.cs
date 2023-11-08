using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdgyElegance.Identity.Entities {
    public class RefreshToken {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Token {  get; set; } = string.Empty;

        public ApplicationUser? User { get; set; } 

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        public DateTime ExpiresIn { get; set; }

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public bool IsRevoked { get; set; } = false;

        public RefreshToken() {
            Token = Guid.NewGuid().ToString();
        }
    }
}

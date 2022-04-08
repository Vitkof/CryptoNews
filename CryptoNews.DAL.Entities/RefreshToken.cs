using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoNews.DAL.Entities
{
    /// <summary>
    /// Models a refresh token.
    /// </summary>
    public class RefreshToken : BaseEntity
    {
        public DateTime ElapsesUtc { get; set; }
        [Required]
        public string Token { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid UserId { get; set; }
        

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

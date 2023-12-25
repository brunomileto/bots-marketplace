using System.ComponentModel.DataAnnotations;

namespace BotMarketplace.API.DTOs.Users
{
    public class UserBaseDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

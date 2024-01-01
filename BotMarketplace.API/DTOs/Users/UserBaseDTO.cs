using System.ComponentModel.DataAnnotations;

namespace BotMarketplace.API.DTOs.Users
{
    public class UserBaseDTO : BaseDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}

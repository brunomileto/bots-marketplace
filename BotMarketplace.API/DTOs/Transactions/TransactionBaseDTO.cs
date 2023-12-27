using System.ComponentModel.DataAnnotations;

namespace BotMarketplace.API.DTOs.Transactions
{
    public class TransactionBaseDTO
    {
        [Required]
        public string ProductId { get; private set; }

        [Required]
        public string BuyerId { get; private set; }

        [Required]
        public string SellerId { get; private set; }

        [Required]
        public decimal Price { get; set; }
    }
}

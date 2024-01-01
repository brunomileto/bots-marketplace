using System.ComponentModel.DataAnnotations;
using BotMarketplace.Core.Enums;

namespace BotMarketplace.API.DTOs.Transactions
{
    public class TransactionBaseDTO : BaseDTO
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string BuyerId { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public EnumTransactionStatus Status { get; set; }
    }
}

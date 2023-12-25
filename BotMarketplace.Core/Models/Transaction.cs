using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMarketplace.Core.Models
{
    public class Transaction : BaseModel
    {
        [Required]
        public string ProductId { get; private set; }

        [Required]
        public string BuyerId { get; private set; }

        [Required]
        public string SellerId { get; private set; }

        public DateTime TransactionDate { get; private set; }

        [Required]
        public decimal Price { get; set; }

        public Transaction ()
        {
            TransactionDate = DateTime.UtcNow;
        }

        public Transaction(DateTime transactionDate)
        {
            TransactionDate = transactionDate;
        }
    }
}

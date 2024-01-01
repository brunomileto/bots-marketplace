using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Enums;

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

        public EnumTransactionStatus Status { get; set; }

        public Transaction (string productId, string buyerId, string sellerId, decimal price)
        {
            TransactionDate = DateTime.UtcNow;
            ProductId = productId;
            BuyerId = buyerId;
            SellerId = sellerId;
            Price = price;
            Status = EnumTransactionStatus.Created;
        }

        public Transaction(string productId, string buyerId, string sellerId, decimal price, DateTime transactionDate)
        {
            TransactionDate = transactionDate;
            ProductId = productId;
            SellerId = sellerId;
            BuyerId = buyerId;
            Price = price;
            Status = EnumTransactionStatus.Created;
        }

        public Transaction(string productId, string buyerId, string sellerId, decimal price, DateTime transactionDate, string id, DateTime dateCreated) : base(id, dateCreated)
        {
            ProductId = productId;
            BuyerId = buyerId;
            SellerId = sellerId;
            TransactionDate = transactionDate;
            Price = price;
            Status = EnumTransactionStatus.Created;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Enums;

namespace BotMarketplace.Core.Models
{
    /// <summary>
    /// Model representing a product.
    /// </summary>
    public class Product : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// Required. The name should be unique and descriptive.
        /// </summary>
        /// <example>SuperWidget</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the product.
        /// Required. This should provide information about the product's features.
        /// </summary>
        /// <example>This widget can perform super actions.</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// Required. The price must be a positive value.
        /// </summary>
        /// <example>99.99</example>
        [Required]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1,000,000.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the creator of the product.
        /// Required. This should be the unique identifier of the creator.
        /// </summary>
        /// <example>creator123</example>
        [Required]
        public string CreatorId { get; set; }

        public EnumProductType ProductType { get; set; }

        public Product()
        {
            ProductType = EnumProductType.BOT;
        }
    }
}

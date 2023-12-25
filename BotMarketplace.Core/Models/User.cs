using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMarketplace.Core.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]

        [EmailAddress]
        public string Email { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public User(string id, DateTime dateCreated, string name, string email) : base(id, dateCreated)
        {
            Name = name;
            Email = email;
        }
    }
}

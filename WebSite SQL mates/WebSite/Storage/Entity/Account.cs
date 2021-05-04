using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSite.Storage.Entity
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Avatar { get; set; }
        [Required]
        public string vkPurchases { get; set; }
        [Required]
        public string gamePurchases { get; set; }
        [Required]
        public string scanPurchases { get; set; }
        [Required]
        public string coinsPurchases { get; set; }
        //public List<string> Purchases;
    }
}

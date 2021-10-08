using System.ComponentModel.DataAnnotations;

namespace Eshop.Core.Models.Database
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImgUri { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace shoping.Models
{
    public partial class Product
    {
        public Product()
        {
            Baskets = new HashSet<Basket>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductSalary { get; set; }
        public int? ProductCategoryId { get; set; }
        public string ProductImg { get; set; }
        public int? ProductUserId { get; set; }
        public string ProductConfirm { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
    }
}

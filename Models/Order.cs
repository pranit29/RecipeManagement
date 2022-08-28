using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeOnlineIngredientSystem.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

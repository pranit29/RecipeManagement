using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeOnlineIngredientSystem.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public int RecipeId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Recipe Recipe { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

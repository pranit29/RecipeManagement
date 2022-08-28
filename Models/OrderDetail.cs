using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeOnlineIngredientSystem.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductDetail Product { get; set; }
    }
}

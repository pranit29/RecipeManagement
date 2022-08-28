using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeOnlineIngredientSystem.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Image { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}

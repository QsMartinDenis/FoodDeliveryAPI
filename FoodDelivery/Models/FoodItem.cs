﻿namespace FoodDelivery.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string FoodItemName { get; set; }
        public decimal FoodItemPrice { get; set; }
        public int FoodCategoryId { get; set; }
        public FoodCategory FoodCategory { get; set; }
    }
}

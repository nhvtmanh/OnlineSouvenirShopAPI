﻿namespace OnlineSouvenirShopAPI.DTOs.ProductDTOs
{
    public class TopSellingProductDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public int SoldQuantity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}

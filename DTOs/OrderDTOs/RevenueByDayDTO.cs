﻿namespace OnlineSouvenirShopAPI.DTOs.OrderDTOs
{
    public class RevenueByDayDTO
    {
        public DateOnly Date { get; set; }
        public decimal Revenue { get; set; }
    }
}
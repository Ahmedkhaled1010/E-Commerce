﻿namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethods deliveryMethods, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId )
        {
            buyerEmail = userEmail;
            shipToAddress = address;
            DeliveryMethods = deliveryMethods;
            Items = items;
            SubTotal = subTotal;
            this.paymentIntentId = paymentIntentId;
        }

        public string buyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; } = default!;
        public DeliveryMethods DeliveryMethods { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
        public int DeliveryMethodsId { get; set; }
        public OrderStatus status { get; set; }
        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethods.Price; }
        public decimal GetTotal() => SubTotal + DeliveryMethods.Cost;
        public string paymentIntentId { get; set; }

    }
}

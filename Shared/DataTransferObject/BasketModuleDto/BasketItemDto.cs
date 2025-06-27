using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.BasketModuleDto
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }

    }
}
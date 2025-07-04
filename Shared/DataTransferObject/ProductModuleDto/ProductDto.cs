﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.ProductModuleDto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default;
        public string Description { get; set; } = null;
        public string PictureUrl { get; set; } = null;
        public decimal Price { get; set; }

        public string productBrand { get; set; } = default;
        public string productType { get; set; } = default;
    }
}

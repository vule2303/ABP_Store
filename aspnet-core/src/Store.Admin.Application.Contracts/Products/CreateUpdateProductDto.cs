﻿using Store.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Products
{
    public class CreateUpdateProductDto
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public ProductType ProductType { get; set; }
        public string SKU { get; set; }
        public int SortOrder { get; set; }
        public bool Visiblity { get; set; }
        public bool IsActive { get; set; }
        public double SellPrice { get; set; } 
        public Guid CategoryId { get; set; }
        public string SeoMetaDescription { get; set; }
        public string Description { get; set; }
        public string ThumbnailPicture { get; set; }
    }
}

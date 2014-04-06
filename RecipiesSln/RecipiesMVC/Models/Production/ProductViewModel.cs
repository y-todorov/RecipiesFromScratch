using RecipiesMVC.DataAnnotations;
using RecipiesModelNS;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipiesMVC.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Relation(EntityType = typeof (UnitMeasure), DataFieldValue = "UnitMeasureId", DataFieldText = "Name")]
        [Display(Name = "Unit Measure")]
        [Required()]
        public int? UnitMeasureId { get; set; }

        [Relation(EntityType = typeof (ProductCategory), DataFieldValue = "CategoryId", DataFieldText = "Name")]
        [Display(Name = "Category")]
        [Required()]

        public int? CategoryId { get; set; }

        [Relation(EntityType = typeof (Store), DataFieldValue = "StoreId", DataFieldText = "Name")]
        [Display(Name = "Store")]
        [Required()]

        public int? StoreId { get; set; }

        [Required(ErrorMessage = "Please enter a name for the product!")]
        [Display(Description="Name of the product.")]
        public string Name { get; set; }

        [Display(Description = "Unique product identification code.")]
        public string Code { get; set; }

        [Range(0, int.MaxValue)]
        //[DataType(DataType.Currency)]
        public decimal? UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        [ReadOnly(true)]
        public double? UnitsInStock { get; set; }

        [ReadOnly(true)]
        [Range(0, int.MaxValue)]
        public decimal? StockValue { get; set; }

        [Range(0, int.MaxValue)]
        [ReadOnly(true)]
        public double? UnitsOnOrder { get; set; }

        [Range(0, int.MaxValue)]
        public double? ReorderLevel { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public ProductViewModel ConvertFromEntity(Product entity)
        {
            ProductId = entity.ProductId;
            UnitMeasureId = entity.UnitMeasureId;
            CategoryId = entity.CategoryId;
            StoreId = entity.StoreId;
            Name = entity.Name;
            Code = entity.Code;
            UnitPrice = Math.Round(entity.UnitPrice.GetValueOrDefault(), 3);
            UnitsInStock = Math.Round(entity.UnitsInStock.GetValueOrDefault(), 3);
            UnitsOnOrder = Math.Round(entity.UnitsOnOrder.GetValueOrDefault(), 3);
            ReorderLevel = Math.Round(entity.ReorderLevel.GetValueOrDefault(), 3);
            StockValue = (decimal) entity.StockValue;
            ModifiedDate = entity.ModifiedDate;
            ModifiedByUser = entity.ModifiedByUser;

            return this;
        }

        public Product ConvertToEntity(Product entity)
        {
            // Do not set read only fields !!!
            entity.ProductId = ProductId;
            entity.UnitMeasureId = UnitMeasureId;
            entity.CategoryId = CategoryId;
            entity.StoreId = StoreId;
            entity.Name = Name;
            entity.Code = Code;
            entity.UnitPrice = Math.Round(UnitPrice.GetValueOrDefault(), 3);
            entity.ReorderLevel = Math.Round(ReorderLevel.GetValueOrDefault(), 3);
            entity.ModifiedDate = ModifiedDate;
            entity.ModifiedByUser = ModifiedByUser;

            return entity;
        }
    }
}
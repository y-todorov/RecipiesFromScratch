using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipiesMVC.Models
{
    public class InventoryViewModel
    {
        [Key]
        public int InventoryId { get; set; }

        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "For Date")]
        [ReadOnly(true)]
        public DateTime? InventoryHeaderForDate { get; set; }

        public decimal? AverageUnitPrice { get; set; }

        [Display(Name = "Qty By Documents")]
        public double? QuantityByDocuments { get; set; }

        [ReadOnly(true)]
        public decimal? ValueByDocuments
        {
            get
            {
                if (!AverageUnitPrice.HasValue || !QuantityByDocuments.HasValue)
                {
                    return null;
                }
                decimal? result = AverageUnitPrice.GetValueOrDefault() * (decimal)QuantityByDocuments.GetValueOrDefault();
                return result;
            }
        }

        [Display(Name = "Stocktake Qty")]
        public double? StocktakeQuantity { get; set; }

        [ReadOnly(true)]
        public decimal? StocktakeValue
        {
            get
            {
                if (!AverageUnitPrice.HasValue || !StocktakeQuantity.HasValue)
                {
                    return null;
                }
                decimal? result = AverageUnitPrice.GetValueOrDefault() * (decimal)StocktakeQuantity.GetValueOrDefault();
                return result;
            }
        }

        [Display(Name = "Deficiency Qty")]
        [ReadOnly(true)]
        public double? DeficiencyQuantity
        {
            get
            {
                if (QuantityByDocuments > StocktakeQuantity)
                {
                    double? result = QuantityByDocuments - StocktakeQuantity;
                    return result;
                }
                return null;
            }
        }

        [ReadOnly(true)]
        public decimal? DeficiencyValue
        {
            get
            {
                if (QuantityByDocuments > StocktakeQuantity)
                {
                    decimal? result = (decimal)DeficiencyQuantity.GetValueOrDefault() * AverageUnitPrice;
                    return result;
                }
                return null;
            }

        }

        [Display(Name = "Surplus Qty")]
        [ReadOnly(true)]
        public double? SurplusQuantity
        {
            get
            {
                if (QuantityByDocuments < StocktakeQuantity)
                {
                    double? result = StocktakeQuantity - QuantityByDocuments;
                    return result;
                }
                return null;
            }
        }

        [ReadOnly(true)]
        public decimal? SurplusValue
        {
            get
            {
                if (QuantityByDocuments < StocktakeQuantity)
                {
                    decimal? result = (decimal)SurplusQuantity.GetValueOrDefault() * AverageUnitPrice;
                    return result;
                }
                return null;
            }
        }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public InventoryViewModel ConvertFromEntity(Inventory entity)
        {
            AverageUnitPrice = entity.AverageUnitPrice;
            //DeficiencyQuantity = entity.DeficiencyQuantity;
            //DeficiencyValue = (decimal?)entity.DeficiencyValue;
            //ForDate = newOrExistingInventoryEntity.ForDate;
            InventoryId = entity.InventoryId;
            ModifiedByUser = entity.ModifiedByUser;
            ModifiedDate = entity.ModifiedDate;
            QuantityByDocuments = entity.QuantityByDocuments;
            StocktakeQuantity = entity.StocktakeQuantity;
            //StocktakeValue = (decimal?)entity.StocktakeValue;
            //SurplusQuantity = entity.SurplusQuantity;
            //SurplusValue = (decimal?)entity.SurplusValue;
            //ValueByDocuments = (decimal?)entity.ValueByDocuments;

            return this;
        }

        public Inventory ConvertToEntity(Inventory entity)
        {
            entity.AverageUnitPrice = AverageUnitPrice;
            entity.DeficiencyQuantity = DeficiencyQuantity;
            entity.DeficiencyValue = (double?)DeficiencyValue;
            //newOrExistingInventoryEntity.ForDate = ForDate;
            entity.InventoryId = InventoryId;
            entity.ModifiedByUser = ModifiedByUser;
            entity.ModifiedDate = ModifiedDate;
            entity.QuantityByDocuments = QuantityByDocuments;
            entity.StocktakeQuantity = StocktakeQuantity;
            entity.StocktakeValue = (double?)StocktakeValue;
            entity.SurplusQuantity = SurplusQuantity;
            entity.SurplusValue = (double?)SurplusValue;
            entity.ValueByDocuments = (double?)ValueByDocuments;

            return entity;
        }
    }
}
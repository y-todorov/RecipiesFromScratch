using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RecipiesModelNS;
using RecipiesMVC.Models;
using RecipiesMVC.Models.Purchasing;

namespace RecipiesMVC.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
         
            Mapper.CreateMap<double?, decimal?>().ConvertUsing<NullableDoubleToNullableDecimalTypeConverter>();
            Mapper.CreateMap<double, decimal?>().ConvertUsing<DoubleToNullableDecimalTypeConverter>();
            
            Mapper.CreateMap<ProductCategory, CategoryViewModel>();
            Mapper.CreateMap<ProductIngredient, ProductIngredientViewModel>();


            Mapper.CreateMap<Product, ProductViewModel>().
                ForMember(m => m.StockValue, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderViewModel>().
                   ForMember(m => m.PurchaseOrderHeaderId, opt => opt.MapFrom(poh => poh.PurchaseOrderId));

            Mapper.CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>().
                ForMember(m => m.Category, opt => opt.MapFrom(o => o.Product.ProductCategory.Name)).
                  ForMember(m => m.LineTotal, opt => opt.Ignore()).
              ForMember(m => m.StockedQuantity, opt => opt.Ignore()); ;

        }

        private class NullableDoubleToNullableDecimalTypeConverter : TypeConverter<double?, decimal?>
        {
            protected override decimal? ConvertCore(double? source)
            {
                decimal? result = (decimal?)source;
                return result;
            }
        }

        private class DoubleToNullableDecimalTypeConverter : TypeConverter<double, decimal?>
        {
            protected override decimal? ConvertCore(double source)
            {
                decimal? result = (decimal?)source;
                return result;
            }
        }
    }
}
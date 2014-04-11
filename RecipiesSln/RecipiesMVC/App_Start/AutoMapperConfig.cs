using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RecipiesModelNS;
using RecipiesMVC.Models;

namespace RecipiesMVC.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            Mapper.CreateMap<ProductCategory, CategoryViewModel>();
            //Mapper.CreateMap<double?, decimal?>().ConvertUsing<NullableDoubleToNullableDecimalTypeConverter>();
            //Mapper.CreateMap<double, decimal?>().ConvertUsing<DoubleToNullableDecimalTypeConverter>();
            Mapper.CreateMap<Product, ProductViewModel>().
                ForMember(m => m.StockValue, opt => opt.Ignore());
            //    opt.MapFrom(p => p.StockValue));// .MapFrom(p => p.StockValue));
        }

        private class NullableDoubleToNullableDecimalTypeConverter : TypeConverter<double?, decimal?>
        {
            protected override decimal? ConvertCore(double? source)
            {
                decimal? result = (decimal?) source;
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
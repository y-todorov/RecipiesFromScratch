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
            Mapper.CreateMap<Product, ProductViewModel>()
                .ForMember(m => m.StockValue, opt => opt.UseValue(123456m));// .MapFrom(p => p.StockValue));
        }
    }
}
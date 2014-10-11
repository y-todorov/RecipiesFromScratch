using System.Linq;
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
            Mapper.CreateMap<decimal?, double?>().ConvertUsing<NullableDecimalToNullableDoubleTypeConverter>();
            Mapper.CreateMap<decimal?, double>().ConvertUsing<NullableDecimalToDoubleTypeConverter>();
            Mapper.CreateMap<double?, decimal?>().ConvertUsing<NullableDoubleToNullableDecimalTypeConverter>();
            Mapper.CreateMap<double, decimal?>().ConvertUsing<DoubleToNullableDecimalTypeConverter>();


            Mapper.CreateMap<ProductCategory, CategoryViewModel>();
            Mapper.CreateMap<ProductIngredient, ProductIngredientViewModel>();
            Mapper.CreateMap<ProductInventoryHeader, ProductInventoryHeaderViewModel>();
            Mapper.CreateMap<ProductInventory, ProductInventoryViewModel>().
                ForMember(m => m.UnitMeasure, opt => opt.MapFrom(poh => poh.Product.UnitMeasure.Name)).
                ForMember(m => m.Category, opt => opt.MapFrom(poh => poh.Product.ProductCategory.Name)).
                ForMember(m => m.InventoryHeaderForDate, opt => opt.MapFrom(poh => poh.ProductInventoryHeader.ForDate));


            Mapper.CreateMap<Inventory, InventoryViewModel>().
                ForMember(m => m.ValueByDocuments, opt => opt.Ignore()).
                ForMember(m => m.StocktakeQuantity, opt => opt.Ignore()).
                ForMember(m => m.DeficiencyQuantity, opt => opt.Ignore()).
                ForMember(m => m.DeficiencyValue, opt => opt.Ignore()).
                ForMember(m => m.SurplusQuantity, opt => opt.Ignore()).
                ForMember(m => m.SurplusValue, opt => opt.Ignore());


            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductViewModel, Product>();

            Mapper.CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderViewModel>().
                ForMember(m => m.SubTotal,
                    opt =>
                        opt.MapFrom(
                            poh => poh.PurchaseOrderDetails.Sum(pod => (double?) pod.UnitPrice*pod.ReceivedQuantity))).
                ForMember(m => m.PurchaseOrderHeaderId, opt => opt.MapFrom(poh => poh.PurchaseOrderId));

            Mapper.CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>().
                ForMember(m => m.Category, opt => opt.MapFrom(o => o.Product.ProductCategory.Name)).
                ForMember(m => m.LineTotal, opt => opt.Ignore()).
                ForMember(m => m.StockedQuantity, opt => opt.Ignore());
            ;

            Mapper.CreateMap<Store, StoreViewModel>();
            Mapper.CreateMap<Recipe, RecipeViewModel>();
            Mapper.CreateMap<UnitMeasure, UnitMeasureViewModel>();

            Mapper.CreateMap<SendGridMail, SendGridMailViewModel>();

            Mapper.CreateMap<Employee, EmployeeViewModel>();
            Mapper.CreateMap<EmployeeViewModel, Employee>();
        }

        public class CustomResolver : ValueResolver<double, decimal>
        {
            protected override decimal ResolveCore(double source)
            {
                return (decimal) source;
            }
        }

        private class DoubleToNullableDecimalTypeConverter : TypeConverter<double, decimal?>
        {
            protected override decimal? ConvertCore(double source)
            {
                var result = (decimal?) source;
                return result;
            }
        }

        private class NullableDecimalToDoubleTypeConverter : TypeConverter<decimal?, double>
        {
            protected override double ConvertCore(decimal? source)
            {
                var result = (double) source.GetValueOrDefault();
                return result;
            }
        }

        private class NullableDecimalToNullableDoubleTypeConverter : TypeConverter<decimal?, double?>
        {
            protected override double? ConvertCore(decimal? source)
            {
                var result = (double?) source;
                return result;
            }
        }

        private class NullableDoubleToNullableDecimalTypeConverter : TypeConverter<double?, decimal?>
        {
            protected override decimal? ConvertCore(double? source)
            {
                var result = (decimal?) source;
                return result;
            }
        }
    }
}
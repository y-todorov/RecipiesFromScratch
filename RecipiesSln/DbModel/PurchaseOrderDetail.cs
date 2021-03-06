//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrderDetail
    {
        public int PurchaseOrderDetailId { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> UnitMeasureId { get; set; }
        public Nullable<double> OrderQuantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public double LineTotal { get; set; }
        public Nullable<double> ReceivedQuantity { get; set; }
        public Nullable<double> ReturnedQuantity { get; set; }
        public double StockedQuantity { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual UnitMeasure UnitMeasure { get; set; }
        public virtual PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}

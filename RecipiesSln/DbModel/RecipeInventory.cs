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
    
    public partial class RecipeInventory
    {
        public int InventoryId { get; set; }
        public Nullable<int> RecipeId { get; set; }
    
        public virtual Recipe Recipe { get; set; }
        public virtual RecipeInventory RecipeInventory1 { get; set; }
        public virtual RecipeInventory RecipeInventory2 { get; set; }
    }
}

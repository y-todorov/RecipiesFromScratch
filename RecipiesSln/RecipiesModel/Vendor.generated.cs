#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using RecipiesModelNS;


namespace RecipiesModelNS	
{
	public partial class Vendor
	{
		private int _vendorId;
		public virtual int VendorId 
		{ 
		    get
		    {
		        return this._vendorId;
		    }
		    set
		    {
		        this._vendorId = value;
		    }
		}
		
		private string _accountNumber;
		public virtual string AccountNumber 
		{ 
		    get
		    {
		        return this._accountNumber;
		    }
		    set
		    {
		        this._accountNumber = value;
		    }
		}
		
		private string _name;
		public virtual string Name 
		{ 
		    get
		    {
		        return this._name;
		    }
		    set
		    {
		        this._name = value;
		    }
		}
		
		private string _address;
		public virtual string Address 
		{ 
		    get
		    {
		        return this._address;
		    }
		    set
		    {
		        this._address = value;
		    }
		}
		
		private string _phone;
		public virtual string Phone 
		{ 
		    get
		    {
		        return this._phone;
		    }
		    set
		    {
		        this._phone = value;
		    }
		}
		
		private string _fax;
		public virtual string Fax 
		{ 
		    get
		    {
		        return this._fax;
		    }
		    set
		    {
		        this._fax = value;
		    }
		}
		
		private string _email;
		public virtual string Email 
		{ 
		    get
		    {
		        return this._email;
		    }
		    set
		    {
		        this._email = value;
		    }
		}
		
		private string _homePage;
		public virtual string HomePage 
		{ 
		    get
		    {
		        return this._homePage;
		    }
		    set
		    {
		        this._homePage = value;
		    }
		}
		
		private DateTime? _modifiedDate;
		public virtual DateTime? ModifiedDate 
		{ 
		    get
		    {
		        return this._modifiedDate;
		    }
		    set
		    {
		        this._modifiedDate = value;
		    }
		}
		
		private string _modifiedByUser;
		public virtual string ModifiedByUser 
		{ 
		    get
		    {
		        return this._modifiedByUser;
		    }
		    set
		    {
		        this._modifiedByUser = value;
		    }
		}
		
		private string _reportAccountNumber;
		public virtual string ReportAccountNumber 
		{ 
		    get
		    {
		        return this._reportAccountNumber;
		    }
		    set
		    {
		        this._reportAccountNumber = value;
		    }
		}
		
		private IList<PurchaseOrderHeader> _purchaseOrderHeaders = new List<PurchaseOrderHeader>();
		public virtual IList<PurchaseOrderHeader> PurchaseOrderHeaders 
		{ 
		    get
		    {
		        return this._purchaseOrderHeaders;
		    }
		}
		
		private IList<ProductVendor> _productVendors = new List<ProductVendor>();
		public virtual IList<ProductVendor> ProductVendors 
		{ 
		    get
		    {
		        return this._productVendors;
		    }
		}
		
	}
}

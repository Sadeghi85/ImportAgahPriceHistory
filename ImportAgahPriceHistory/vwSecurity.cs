//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImportAgahPriceHistory
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwSecurity
    {
        public int SecurityID { get; set; }
        public string SecuritySymbol { get; set; }
        public string SecurityName { get; set; }
        public string SecurityDescription { get; set; }
        public string MarketType { get; set; }
        public string SecurityType { get; set; }
        public int MarketTypeID { get; set; }
        public int SecurityTypeID { get; set; }
        public Nullable<int> AgahID { get; set; }
        public Nullable<int> AgahSecurityID { get; set; }
        public long TseID { get; set; }
        public string SecurityGroupTitle { get; set; }
        public int SecurityGroupID { get; set; }
        public string Status { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusDescription { get; set; }
        public int Rahavard365ID { get; set; }
    }
}

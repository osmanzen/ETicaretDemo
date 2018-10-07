using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMCategoryProduct
    {
        public Guid     ProductID       { get; set; }
        public Guid     ModelID         { get; set; }
        public Guid     MakeID          { get; set; }
        public string   ProductName     { get; set; }
        public decimal  UnitPrice       { get; set; }
        public int      UnitsInStock    { get; set; }
        public int      ViewCount       { get; set; }
        public string   Description     { get; set; }
        public bool     IsActive        { get; set; }
        public decimal  TotalPrice      { get; set; }
        public decimal  OldPrice        { get; set; }
        public string   TotalDiscount   { get; set; }
        public bool     hasCampaign     { get; set; }
        public string   PicturePath     { get; set; }
        public string   ModelName       { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentCars
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarPriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarPriceList()
        {
            this.CarsInventories = new HashSet<CarsInventory>();
        }
    
        public int CarPriceListId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int DelayPrice { get; set; }
        public int Year { get; set; }
        public bool Gear { get; set; }
        public string CarGroup { get; set; }
        public string Image { get; set; }
    
        public virtual CarGroup CarGroup1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarsInventory> CarsInventories { get; set; }
    }
}

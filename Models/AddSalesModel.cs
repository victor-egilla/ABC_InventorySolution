using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Company_Solution.Models
{
    public class AddSalesModel
    {
        public string CustomerName { get; set; }
        public int CityCode { get; set; }
        public string CountryCode { get; set; }
        public string RegionCode { get; set; }
        public int ProductID { get; set; }
        public DateTime DateofSale { get; set; }
        public int ProductQuantity { get; set; }
    }
}

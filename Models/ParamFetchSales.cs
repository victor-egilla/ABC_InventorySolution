using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Company_Solution.Models
{
    public class ParamFetchSales
    {
        public int draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }
        public DateTime Date { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
        public int CityCode { get; set; }
    }
}

using ABC_Company_Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Company_Solution.Interfaces
{
    public interface ISalesRepository
    {
        Task<dynamic> GetDropDownData(int TableType, string QueryId);
        Task<dynamic> AddSalesInventory(AddSalesModel payload);
        Task<dynamic> GetAllSales(ParamFetchSales payload);
        Task<dynamic> GetSalesDetailsBySalesId(int SalesId);
    }
}

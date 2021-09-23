using ABC_Company_Solution.Helper_Classes;
using ABC_Company_Solution.Helper_Methods;
using ABC_Company_Solution.Interfaces;
using ABC_Company_Solution.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Company_Solution.Repositories
{
    public class SalesRepository : ISalesRepository
    {

        private readonly ErrorLogger _logger;
        private readonly string _connectionString;
        private readonly HelperMethods _webHelpers;

        public SalesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = new ErrorLogger(_Destination: @"ErrorLogs");
            _webHelpers = new HelperMethods(configuration);
        }
        public async Task<dynamic> GetDropDownData(int TableType, string QueryId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@TableType", TableType);
                    param.Add("@QueryId", QueryId);

                    //var response = await SqlMapper.QueryMultipleAsync(con, "sp_GetAllActiveBlogPost", param, commandType: CommandType.StoredProcedure);
                    var response = await SqlMapper.QueryAsync<dynamic>(con, "sp_GetDropdownData", param, commandType: CommandType.StoredProcedure);
                    if (response.Count() > 0)
                    {
                        res.IsSuccess = true;
                        res.ResponseCode = ((int)Utilities.ResponseCode.Success).ToString().PadLeft(2, '0');
                        res.ResponseMessage = "Success";
                        res.ResponseData = response;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex);
                await _webHelpers.LogError(ex.ToString());
                res.IsSuccess = false;
                res.ResponseCode = ((int)Utilities.ResponseCode.Failure).ToString().PadLeft(2, '0');
                res.ResponseMessage = $"Failed";
            }

            return res;
        }


        //ADD SALES INFORMATION
        public async Task<dynamic> AddSalesInventory(AddSalesModel payload)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@CustomerName", payload.CustomerName);
                    param.Add("@CityCode", payload.CityCode);
                    param.Add("@CountryCode", payload.CountryCode);
                    param.Add("@RegionCode", payload.RegionCode);
                    param.Add("@ProductId", payload.ProductID);
                    param.Add("@DateOfSale", payload.DateofSale);
                    param.Add("@Quantity", payload.ProductQuantity);

                    var response = await SqlMapper.QueryAsync<dynamic>(con, "sp_AddSalesInfo", param, commandType: CommandType.StoredProcedure);
                    var responseCode = response.Select(x => x.Response).FirstOrDefault();
                    
                    if (responseCode > 0)
                    {
                        res.IsSuccess = true;
                        res.ResponseCode = ((int)Utilities.ResponseCode.Success).ToString().PadLeft(2, '0');
                        res.ResponseMessage = "Success";
                        res.ResponseData = response;
                    }
                    else
                    {
                        res.IsSuccess = false;
                        res.ResponseCode = ((int)Utilities.ResponseCode.Failure).ToString().PadLeft(2, '0');
                        res.ResponseMessage = "Failed to insert";
                        res.ResponseData = response;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex);
                await _webHelpers.LogError(ex.ToString());
                res.IsSuccess = false;
                res.ResponseCode = ((int)Utilities.ResponseCode.Failure).ToString().PadLeft(2, '0');
                res.ResponseMessage = $"Failed";
            }

            return res;
        }

        //GET ALL SALES
        public async Task<dynamic> GetAllSales(ParamFetchSales payload)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@start", payload.Start);
                    param.Add("@length", payload.Length);
                    param.Add("@search", payload.Search);
                    param.Add("@Date", payload.Date);
                    param.Add("@CountryCode", payload.CountryCode);
                    param.Add("@State", payload.State);
                    param.Add("@CityCode", payload.CityCode);
                   

                    var response = await SqlMapper.QueryMultipleAsync(con, "sp_GetAllSales", param, commandType: CommandType.StoredProcedure);
                    if (response != null)
                    {
                        var PaginationDetails = response.Read<dynamic>().FirstOrDefault();
                        var SalesList = response.Read<dynamic>();
                        dynamic dataToReturn = new ExpandoObject();
                        dataToReturn.draw = payload.draw;
                        dataToReturn.recordsTotal = PaginationDetails.RecordsTotal;
                        dataToReturn.data = SalesList.ToList();
                        dataToReturn.recordsFiltered = PaginationDetails.RecordsFiltered;
                        dataToReturn.ResponseCode = ((int)Utilities.ResponseCode.Success).ToString().PadLeft(2, '0');
                        dataToReturn.IsSuccess = false;
                        dataToReturn.ResponseMessage = $"Success";

                        return dataToReturn;
                    }
                    else
                    {
                        res.IsSuccess = false;
                        res.ResponseCode = ((int)Utilities.ResponseCode.Failure).ToString().PadLeft(2, '0');
                        res.ResponseMessage = $"Failed";

                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex);
                await _webHelpers.LogError(ex.ToString());
                res.IsSuccess = false;
                res.ResponseCode = ((int)Utilities.ResponseCode.Failure).ToString().PadLeft(2, '0');
                res.ResponseMessage = $"Failed";
             
                return res;
            }


        }

        //GET SALES DETAILS
        public async Task<dynamic> GetSalesDetailsBySalesId(int SalesId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@salesID", SalesId);

                    //var response = await SqlMapper.QueryMultipleAsync(con, "sp_GetAllActiveBlogPost", param, commandType: CommandType.StoredProcedure);
                    var response = await SqlMapper.QueryAsync<dynamic>(con, "sp_GetSalesDetailBySalesId", param, commandType: CommandType.StoredProcedure);
                    if (response.Count() > 0)
                    {
                        res.IsSuccess = true;
                        res.ResponseCode = ((int)Utilities.ResponseCode.Success).ToString().PadLeft(2, '0');
                        res.ResponseMessage = "Success";
                        res.ResponseData = response;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex);
                await _webHelpers.LogError(ex.ToString());
                res.IsSuccess = false;
                res.ResponseCode = ((int)Utilities.ResponseCode.Failure).ToString().PadLeft(2, '0');
                res.ResponseMessage = $"Failed";
            }
            return res;
        }

    }
}


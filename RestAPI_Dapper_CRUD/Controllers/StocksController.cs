using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using RestAPI_Dapper_CRUD.Models.Stocks;

namespace RestAPI_Dapper_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly string _connectionString = "Server=.;Database=StockDB;User Id=sa;Password=23032106;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetStocks()
        {          
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tbl_Stock WHERE deleteFlag = 0";
                List<StockReqModel> Stocks = db.Query<StockReqModel>(query).ToList();

                if (Stocks == null || Stocks.Count == 0)
                {
                    return NotFound("Stock Not Found");
                }

                return Ok(Stocks);
            }
          
        }

        [HttpGet("{id}")]
        public IActionResult GetStock(int id)
        {           
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM Tbl_Stock WHERE deleteFlag = 0 AND id = { id }";
                List<StockReqModel> Stocks = db.Query<StockReqModel>(query).ToList();

                if (Stocks == null || Stocks.Count == 0)
                {
                    return NotFound("Stock Not Found");
                }

                return Ok(Stocks);
            }         
        }

        [HttpPost]
        public IActionResult CreateStock(StockResModel stockResModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"INSERT INTO Tbl_Stock (code, description, purchasePrice, salePrice, deleteFlag) 
                                VALUES ('{ stockResModel.Code }'
                                , '{ stockResModel.Description }'
                                , { stockResModel.PurchasePrice }
                                , { stockResModel.SalePrice }
                                , 0)";
                var result = db.Execute(query);                           

                if (result > 0)
                {
                    return Ok("Created Successfully");
                }
                else
                {
                    return BadRequest("Creation Failed");
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock(int id, StockResModel stockResModel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string existQuery = $"SELECT * FROM Tbl_Stock WHERE deleteFlag = 0 AND id = {id}";
                List<StockReqModel> Stocks = db.Query<StockReqModel>(existQuery).ToList();

                if (Stocks == null || Stocks.Count == 0)
                {
                    return NotFound("Stock Not Found");
                }

                string query = $@"UPDATE Tbl_Stock 
                                SET code = '{ stockResModel.Code }'
                                , description = '{ stockResModel.Description }'
                                , purchasePrice = { stockResModel.PurchasePrice }
                                , salePrice = { stockResModel.SalePrice }
                                WHERE id = { id }";
                var result = db.Execute(query);
                if (result > 0)
                {
                    return Ok("Updated Successfully");
                }
                else
                {
                    return BadRequest("Updation Failed");
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string existQuery = $"SELECT * FROM Tbl_Stock WHERE deleteFlag = 0 AND id = {id}";
                List<StockReqModel> Stocks = db.Query<StockReqModel>(existQuery).ToList();

                if (Stocks == null || Stocks.Count == 0)
                {
                    return NotFound("Stock Not Found");
                }

                string query = $@"UPDATE Tbl_Stock 
                                SET deleteFlag = 1
                                WHERE id = { id }";
                var result = db.Execute(query);
                if (result > 0)
                {
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return BadRequest("Deletion Failed");
                }
            }
        }
    }
}
namespace RestAPI_Dapper_CRUD.Models.Stocks
{
    public class StockReqModel
    {
        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; } = 0;

        public decimal SalePrice { get; set; } = 0;
    }
}
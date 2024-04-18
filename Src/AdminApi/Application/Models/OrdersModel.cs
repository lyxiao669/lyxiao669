namespace AdminApi.Application
{
    public class OrdersModel : PageModel
    {
        /// <summary>
        /// 订单状态，-1 表示查询所有状态的订单
        /// </summary>
        public int Status { get; set; } = -1; 
    }
}

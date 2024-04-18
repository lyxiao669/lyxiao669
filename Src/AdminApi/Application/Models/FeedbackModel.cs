namespace AdminApi.Application
{
    public class FeedbackModel : PageModel
    {
        /// <summary>
        /// 订单状态，-1 表示查询所有
        /// </summary>
        public int Status { get; set; } = -1; 
    }
}

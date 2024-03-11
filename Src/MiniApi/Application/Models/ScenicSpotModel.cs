namespace MiniApi.Application
{
    public class ScenicSpotModel:PageModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string cityName { get; set; }
    }
}

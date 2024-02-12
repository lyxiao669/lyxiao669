using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{

    /// <summary>
    /// C:command，对外的功能（单一职责）
    /// E:event,代码解耦合
    /// Q:query,查询
    /// R:Respository，存储
    /// S:Service，公用的功能
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// 照片队列
        /// </summary>
        void EnPhotoEnqueue(string photo);

        /// <summary>
        /// 移除的照片
        /// </summary>
        string Dequeue();
    }
    public class PhotoService : IPhotoService
    {
        private ConcurrentQueue<string> _photoConcurrentQueue;

        public PhotoService()
        {
            _photoConcurrentQueue = new ConcurrentQueue<string>();
        }


        public void EnPhotoEnqueue(string photo)
        {
            _photoConcurrentQueue.Enqueue(photo);
        }

        public string Dequeue()
        {
            _photoConcurrentQueue.TryDequeue(out var img);
            return img;
        }
    }
}

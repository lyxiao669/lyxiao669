using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Infrastructure.Services
{
    public class IdWorker : IIdWorker
    {
        private static long _workerId;//机器ID
        private readonly static long _twepoch = 687888001020L; //唯一时间，这是一个避免重复的随机量，自行设定不要大于当前时间戳
        private static long _sequence = 0L;
        private readonly static int _workerIdBits = 4; //机器码字节数。4个字节用来保存机器码(定义为Long类型会出现，最大偏移64位，所以左移64位没有意义)
        private readonly static long _maxWorkerId = -1L ^ -1L << _workerIdBits; //最大机器ID
        private readonly static int _sequenceBits = 10; //计数器字节数，10个字节用来保存计数码
        private readonly static int _workerIdShift = _sequenceBits; //机器码数据左移位数，就是后面计数器占用的位数
        private readonly static int _timestampLeftShift = _sequenceBits + _workerIdBits; //时间戳左移动位数就是机器码和计数器总字节数
        private readonly static long _sequenceMask = -1L ^ -1L << _sequenceBits; //一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        private long _lastTimestamp = -1L;

        /// <summary>
        /// 机器码
        /// </summary>
        /// <param name="workerId"></param>
        public IdWorker(long workerId)
        {
            if (workerId > _maxWorkerId || workerId < 0)
                throw new Exception(string.Format("worker Id can't be greater than {0} or less than 0 ", workerId));
            _workerId = workerId;
        }
        public string NextIdString()
        {
            return NextId().ToString();
        }
        public string NextIdString(string prefix)
        {
            return prefix + NextId().ToString();
        }
        public long NextId()
        {
            lock (this)
            {
                long timestamp = TimeStamp();
                if (this._lastTimestamp == timestamp)
                { //同一微妙中生成ID
                    _sequence = (_sequence + 1) & _sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                    if (_sequence == 0)
                    {
                        //一微妙内产生的ID计数已达上限，等待下一微妙
                        timestamp = TillNextMillis(this._lastTimestamp);
                    }
                }
                else
                {  //不同微秒生成ID
                    _sequence = 0; //计数清0
                }
                if (timestamp < _lastTimestamp)
                { //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
                    throw new Exception(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                        _lastTimestamp - timestamp));
                }
                _lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
                long nextId = (timestamp - _twepoch << _timestampLeftShift) | _workerId << _workerIdShift | _sequence;
                return nextId;
            }
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private static long TillNextMillis(long lastTimestamp)
        {
            long timestamp = TimeStamp();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeStamp();
            }
            return timestamp;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private static long TimeStamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}

using System;
using Framework.TimerTask.Interface;
using GameFramework;
using GameFramework.Helper;


namespace Framework.TimerTask {

    public class TimingWheelTimer : ITimer {

        /// <summary>
        /// 时间轮
        /// </summary>
        private readonly TimingWheel _timingWheel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickSpan">时间槽大小，毫秒</param>
        /// <param name="slotCount">时间槽数量</param>
        /// <param name="startMs">起始时间戳，标识时间轮创建时间</param>
        private TimingWheelTimer( long tickSpan, int slotCount, long startMs ) {
            _timingWheel = new TimingWheel(tickSpan, slotCount, startMs);
        }

        /// <summary>
        /// 任务总数
        /// </summary>
        public int TaskCount { get; }

        /// <summary>
        /// 构建时间轮计时器
        /// </summary>
        /// <param name="tickSpan">时间槽大小</param>
        /// <param name="slotCount">时间槽数量</param>
        /// <param name="startMs">起始时间戳，标识时间轮创建时间，默认当前时间</param>
        public static ITimer Build( TimeSpan tickSpan, int slotCount, long? startMs = null ) {
            return new TimingWheelTimer((long)tickSpan.TotalMilliseconds, slotCount,
                                        startMs ?? Game.TimeStampMillisecond);
        }

        public ITimeTask AddTask( TimeSpan timeout, Action delegateTask ) { throw new NotImplementedException(); }

        public ITimeTask AddTask( long timeoutMs, Action delegateTask ) { throw new NotImplementedException(); }

    }

}
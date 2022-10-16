using System;
using Framework.TimerTask.Interface;
using GameFramework;
using GameFramework.Extensions;
using GameFramework.Helper;
using Unity.VisualScripting;


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
            _timingWheel = new TimingWheel(tickSpan, slotCount, startMs, _taskCount);
        }

        /// <summary>
        /// 任务总数
        /// </summary>
        public int TaskCount => _taskCount.Get();

        private readonly AtomicInt _taskCount = new AtomicInt();

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

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="delegateTask"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ITimeTask AddTask( TimeSpan timeout, Action delegateTask ) {
            ErrorCheck.NotNull(delegateTask, nameof(delegateTask));
            var timeoutMs = Game.TimeStampMillisecond + (long)timeout.TotalMilliseconds;
            return AddTask(timeoutMs, delegateTask);
        }

        public ITimeTask AddTask( long timeoutMs, Action delegateTask ) {
            ErrorCheck.NotNull(delegateTask, nameof(delegateTask));

            var task = new TimeTask(timeoutMs, delegateTask);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="timeTask">延时任务</param>
        private void AddTask( TimeTask timeTask ) {
            // 添加失败，说明该任务已到期，需要执行了
            if( !_timingWheel.AddTask(timeTask) ) {
                if( timeTask.TaskStatus == TimeTaskStatus.Wait ) {
                }
            }
        }

    }

}
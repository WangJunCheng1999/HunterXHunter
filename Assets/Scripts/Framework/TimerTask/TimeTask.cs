using System;
using Framework.TimerTask.Interface;
using GameFramework;


namespace Framework.TimerTask {

    public class TimeTask : ITimeTask {

        /// <summary>
        /// 过期时间
        /// </summary>
        public long TimeoutMs { get; }

        /// <summary>
        /// 任务
        /// </summary>
        public Action Task { get; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TimeTaskStatus TaskStatus { get; private set; }

        public TimeSlot TimeSlot { get; private set; }

        public bool Cancel() { return false; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout">过期时间，相对时间</param>
        /// <param name="delayTask">延时任务</param>
        public TimeTask( TimeSpan timeout, Action delayTask ) {
            TimeoutMs = Game.TimeStampMillisecond + (long)timeout.TotalMilliseconds;
            Task = delayTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutMs">过期时间戳，绝对时间</param>
        /// <param name="delayTask">延时任务</param>
        public TimeTask( long timeoutMs, Action delayTask ) {
            TimeoutMs = timeoutMs;
            Task = delayTask;
        }

        /// <summary>
        /// 运行
        /// </summary>
        public void Run() {
            if( TaskStatus == TimeTaskStatus.Wait ) {
                TaskStatus = TimeTaskStatus.Running;
                Remove();
            }

            if( TaskStatus == TimeTaskStatus.Running ) {
                try {
                    Task?.Invoke();
                    TaskStatus = TimeTaskStatus.Success;
                }
                catch {
                    TaskStatus = TimeTaskStatus.Fail;
                }
            }

        }

        /// <summary>
        /// 移除
        /// </summary>
        public void Remove() {
            if( TimeSlot != null && !TimeSlot.RemoveTask(this) ) {

            }

            TimeSlot = null;
        }

    }

}
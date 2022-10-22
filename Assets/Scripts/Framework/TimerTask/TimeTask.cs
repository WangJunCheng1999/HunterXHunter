using System;
using Framework.TimerTask.Interface;
using GameFramework;


namespace Framework.TimerTask {

    public class TimeTask : ITimeTask {

        /// <summary>
        /// 间隔时间
        /// </summary>
        private TimeSpan Span { get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long TimeoutMs { get; private set; }

        /// <summary>
        /// 任务回调
        /// </summary>
        private Action Task { get; }

        /// <summary>
        /// 时间槽
        /// </summary>
        public TimeSlot TimeSlot { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public TimeTaskType Type { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TimeTaskStatus TaskStatus { get; private set; }

        public TimeTask( long timeStamp, TimeSpan span, Action delayTask ) {
            Span = span;
            TimeoutMs = timeStamp + (long)Span.TotalMilliseconds;
            Task = delayTask;
        }

        public TimeTask( long timeoutMs, Action delayTask ) {
            TimeoutMs = timeoutMs;
            Task = delayTask;
        }

        /// <summary>
        /// 根据传入的时间戳刷新时间
        /// </summary>
        /// <param name="timeStamp"></param>
        public void RefreshTimeout( long timeStamp ) { TimeoutMs = timeStamp + Span.Milliseconds; }

        /// <summary>
        /// 取消任务
        /// </summary>
        /// <returns></returns>
        public bool Cancel() {
            Remove();
            TaskStatus = TimeTaskStatus.Cancel;
            return true;
        }

        /// <summary>
        /// 运行
        /// </summary>
        public void Run() {
            if( TaskStatus != TimeTaskStatus.Wait ) {
                return;
            }

            Remove();
            Task?.Invoke();
            TaskStatus = TimeTaskStatus.Done;
        }

        /// <summary>
        /// 移除
        /// </summary>
        private void Remove() {
            if( TaskStatus != TimeTaskStatus.Wait ) {
                return;
            }

            TimeSlot?.RemoveTask(this);
            TimeSlot = null;
        }

    }

}
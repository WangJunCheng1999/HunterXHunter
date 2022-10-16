using System;
using Framework.TimerTask.Interface;


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
        public TimeTaskStatus TaskStatus { get; }

        public TimeSlot TimeSlot { get; }

        public bool Cancel() { return false; }

    }

}
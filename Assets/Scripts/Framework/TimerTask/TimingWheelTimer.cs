using System;
using Framework.TimerTask.Interface;
using GameFramework;
using GameFramework.Extensions;
using GameFramework.Helper;
using Unity.VisualScripting;


namespace Framework.TimerTask {

    public class TimingWheelTimer : ITimer {

        // 时间轮
        private readonly TimingWheel _timingWheel;

        // 当前任务数
        private readonly AtomicInt _taskCount = new();

        // 时间戳获取
        private readonly IDateTimeHelper _dateTimeHelper = new DateTimeHelper();

        /// <summary>
        /// 任务总数
        /// </summary>
        public int TaskCount => _taskCount.Get();

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

        /// <summary>
        /// 添加循环任务
        /// </summary>
        /// <param name="loopDuration"></param>
        /// <param name="delegateTask"></param>
        /// <returns></returns>
        public ITimeTask AddLoopTask( TimeSpan duration, Action delegateTask ) {
            ErrorCheck.NotNull(delegateTask, nameof(delegateTask));
            ErrorCheck.NotNull(duration, nameof(duration));

            return AddTask(new TimeTask(_dateTimeHelper.GetTimestamp(), duration, delegateTask));
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="timeoutMs"></param>
        /// <param name="delegateTask"></param>
        /// <returns></returns>
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
        private ITimeTask AddTask( TimeTask timeTask ) {
            // 添加失败，说明该任务已到期，需要执行了
            if( !_timingWheel.AddTask(timeTask) ) {
                Game.Logger.Warning($"task:{timeTask.TimeoutMs}添加失败");
            }

            return timeTask;
        }

        /// <summary>
        /// 推进时间轮
        /// </summary>
        /// <param name="timeStamp"> 当前的时间戳 </param>
        public void Step( long timeStamp ) { _timingWheel.Step(timeStamp, HandleTaskTrigger); }

        /// <summary>
        /// 处理任务触发事件
        /// </summary>
        /// <param name="task"></param>
        private void HandleTaskTrigger( TimeTask task ) {
            if( task.Type == TimeTaskType.Loop ) {
                task.RefreshTimeout(_dateTimeHelper.GetTimestamp());
                AddTask(task);
            }

            task.Run();
        }

    }

}
using System;
using GameFramework;
using GameFramework.Extensions;
using GameFramework.Helper;


namespace Framework.TimerTask {

    public class TimingWheel {

        // 槽跨度
        private readonly long _tickSpan;

        // 槽数
        private readonly int _slotCount;

        // 时间轮跨度
        private readonly long _wheelSpan;

        // 时间槽
        private readonly TimeSlot[ ] _timeSlots;

        // 当前槽下表
        private int _currentIndex;

        // 当前时间戳
        private long _currentNeedle;

        // 任务数
        private AtomicInt _taskCount;

        // 下一个时间轮
        private TimingWheel _nextWheel;

        // 上一个时间轮
        private TimingWheel _previousWheel;

        /// <summary>
        /// 创建时间轮
        /// </summary>
        /// <param name="tickSpan"> 槽间隔 </param>
        /// <param name="slotCount"> 槽数 </param>
        /// <param name="startMs"> 起始时间 </param>
        /// <param name="taskCount"> 任务数量 </param>
        public TimingWheel( long tickSpan, int slotCount, long startMs, AtomicInt taskCount ) {
            _tickSpan = tickSpan;
            _slotCount = slotCount;
            _taskCount = taskCount;
            _wheelSpan = _slotCount * _tickSpan;
            _timeSlots = new TimeSlot[_slotCount];

            for( int i = 0 ; i < _timeSlots.Length ; i++ ) {
                _timeSlots[i] = new TimeSlot(this, _taskCount);
            }

            SetNeedle(startMs);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="timeTask"></param>
        /// <returns></returns>
        public bool AddTask( TimeTask timeTask ) {
            if( timeTask.TaskStatus == TimeTaskStatus.Wait ) {
                return false;
            }

            long duration = timeTask.TimeoutMs - _currentNeedle;

            // 当前时间已经远远大于任务触发的时间了,需要直接触发
            // if( duration <= 0 ) {
            //     timeTask.Run();
            //     return false;
            // }

            // 如果这是外圈,并且当前的任务过期时间小于一个TickSpan
            // 那么说明这个任务会出现在更内圈的时间轮里
            if( _previousWheel != null && duration <= _tickSpan ) {
                return _previousWheel.AddTask(timeTask);
            }

            if( duration <= _wheelSpan ) {
                // 当前任务过期时间在这个时间轮能控制的范围内
                var targetIndex = (_currentIndex + (int)((duration - 1) / _tickSpan)) % _slotCount;
                _timeSlots[targetIndex].AddTask(timeTask);
                return true;
            }

            // 当前任务过期时间在这个时间轮不能控制的范围内
            CreateNextWheel();
            return _nextWheel.AddTask(timeTask);

        }

        /// <summary>
        /// 推进时间轮
        /// </summary>
        /// <param name="timeStamp">当前的时间戳</param>
        /// <param name="action">触发的回调</param>
        public void Step( long timeStamp, Action<TimeTask> action ) {
            while( _currentNeedle + _tickSpan <= timeStamp ) {
                // 进入到循环，代表这个Slot已经走完，所以要释放Slot
                SetNeedle(_currentNeedle + _tickSpan);

                // 如果没有前一个时间轮,那么这将是最小的一个时间轮,最小的时间轮第一个格子是可用的
                // 所以可用从第一格子开始开始激活(也就是不用先跳到下一个下标当中
                // 外圈的时间轮第一个格子(Index = 0 )是内圈的整个WheelSpan
                // 所以外圈的轮子都是从下一个开始转起,所以要先到达下一格并且把当前槽的所有时间任务都放到上一个时间轮中
                if( _previousWheel != null ) {
                    NextIndex();
                }

                _timeSlots[_currentIndex].Trigger(action);

                // 这是最内圈的时间轮,在当前槽触发后再到下一个下标当中
                if( _previousWheel == null ) {
                    NextIndex();
                }

                // 当前时间轮的事物处理完毕后,将处理下一个
                _nextWheel?.Step(_currentNeedle, (task => AddTask(task)));
            }
        }

        /// <summary>
        /// 下一个索引
        /// </summary>
        private void NextIndex() {
            _currentIndex++;
            _currentIndex %= _slotCount;
        }

        /// <summary>
        /// 设置指针
        /// </summary>
        /// <param name="timestamp"></param>
        private void SetNeedle( long timestamp ) {
            // 修剪为tickSpan的整数倍
            _currentNeedle = timestamp - timestamp % _tickSpan;
        }

        /// <summary>
        /// 创建下一个指针
        /// </summary>
        private void CreateNextWheel() {
            if( _nextWheel != null ) {
                return;
            }

            _nextWheel = new TimingWheel(_wheelSpan, _slotCount, _currentNeedle, _taskCount);
            _nextWheel._previousWheel = this;
        }

    }

}
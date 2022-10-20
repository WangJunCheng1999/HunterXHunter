using System;
using GameFramework;
using GameFramework.Extensions;
using GameFramework.Helper;


namespace Framework.TimerTask
{
    internal class TimingWheel
    {
        private readonly long _tickSpan;
        private readonly int _slotCount;
        private readonly long _wheelSpan;
        private readonly TimeSlot[] _timeSlots;

        /// <summary>
        /// 当前指针，标识当前时间槽的时间戳，是tickSpan的整数倍
        /// <para>指针指向的时间槽，就是刚好到期的时间槽</para>
        /// <para>当前槽的范围为：[currentNeedle, currentNeedle + tickSpan)</para>
        /// </summary>
        private long _currentNeedle;

        private TimingWheel _nextWheel;

        private AtomicInt _taskCount;

        public int TaskCount => 0;

        public bool AddTask(TimeTask timeTask)
        {
           // if (timeTask.TaskStatus == TimeTaskStatus.Wait)
            // {
            //     Game.Logger.Warning($"时间任务重复添加");
            //     return false;
            // }
            if (timeTask.TimeoutMs <= _currentNeedle + _wheelSpan)
            {
                int targetIndex = (int)(_currentIndex + Math.Max(timeTask.TimeoutMs - _currentNeedle, 0) / _tickSpan) %
                                  _slotCount;
                _timeSlots[targetIndex].AddTask(timeTask);
                Game.Logger.Log($"真实时间：{new DateTimeHelper().GetTimestamp()}");
                Game.Logger.Log($"轮子时间：{_currentNeedle}");
                Game.Logger.Log($"任务时间：{timeTask.TimeoutMs}");
                Game.Logger.Log($"当前Index:{_currentIndex},添加到的Index:{targetIndex}");
                Game.Logger.Log($"TimeWheelSpan:{_wheelSpan},相差时间：{timeTask.TimeoutMs - _currentNeedle}");
                Game.Logger.Log($"TickSpan:{_tickSpan}");
            }
            else
            {
                
                Game.Logger.Log($"timeTick:{_tickSpan}传入下一轮");
                CreateNextWheel();
                _nextWheel.AddTask(timeTask);
            }


            return true;
        }

        private int _currentIndex = 0;

        public TimingWheel(long tickSpan, int slotCount, long startMs, AtomicInt taskCount)
        {
            _tickSpan = tickSpan;
            _slotCount = slotCount;
            _taskCount = taskCount;
            _wheelSpan = _slotCount * _tickSpan;
            _timeSlots = new TimeSlot[_slotCount];
            InitSlots();
            SetNeedle(startMs);
        }

        private void InitSlots()
        {
            for (int i = 0; i < _timeSlots.Length; i++)
            {
                _timeSlots[i] = new TimeSlot();
                _timeSlots[i].TaskCount = _taskCount;
            }
        }

        public void Step(long timeStamp, Action<TimeTask> action)
        {
            while (_currentNeedle + _tickSpan <= timeStamp)
            {
                // 进入到循环，代表这个Slot已经走完，所以要释放Slot
                SetNeedle(_currentNeedle + _tickSpan);
                _nextWheel?.Step(_currentNeedle, (task => AddTask(task)));
                _timeSlots[_currentIndex].Trigger(action);
                _currentIndex = (_currentIndex + 1) % _slotCount;
            }
        }

        /// <summary>
        /// 设置指针
        /// </summary>
        /// <param name="timestamp"></param>
        private void SetNeedle(long timestamp)
        {
            // 修剪为tickSpan的整数倍
            _currentNeedle = timestamp - (timestamp % _tickSpan);
        }

        private void CreateNextWheel()
        {
            if (_nextWheel != null)
            {
                return;
            }

            _nextWheel = new TimingWheel(_wheelSpan, _slotCount, _currentNeedle, _taskCount);
        }
    }
}
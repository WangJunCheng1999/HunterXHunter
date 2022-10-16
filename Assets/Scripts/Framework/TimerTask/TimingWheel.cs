using System;
using GameFramework.Extensions;


namespace Framework.TimerTask {

    internal class TimingWheel {

        private readonly long _tickSpan;
        private readonly int _slotCount;
        private readonly long _wheelSpan;
        private readonly TimeSlot[ ] _timeSlots;
        /// <summary>
        /// 当前指针，标识当前时间槽的时间戳，是tickSpan的整数倍
        /// <para>指针指向的时间槽，就是刚好到期的时间槽</para>
        /// <para>当前槽的范围为：[currentNeedle, currentNeedle + tickSpan)</para>
        /// </summary>
        private long _currentNeedle;
        private TimingWheel _nextWheel;

        private AtomicInt _taskCount;

        public int TaskCount => 0;

        public bool AddTask( TimeTask timeTask ) { return false; }

        public TimingWheel( long tickSpan, int slotCount, long startMs, AtomicInt taskCount ) {
            _tickSpan = tickSpan;
            _slotCount = slotCount;
            _taskCount = taskCount;

            _wheelSpan = _slotCount * _tickSpan;
            _timeSlots = new TimeSlot[_slotCount];

            foreach( var slot in _timeSlots ) {
                slot.TaskCount = _taskCount;
            }
        }

        public static void Build( int a ) { }

        private void CreateNextWheel() {
            if( _nextWheel != null ) {
                return;
            }

            _nextWheel = new TimingWheel(_wheelSpan, _slotCount, _currentNeedle, _taskCount);

        }

    }

}
using System;


namespace Framework.TimerTask {

    internal class TimingWheel {

        private readonly long _tickSpan;
        private readonly int _slotCount;

        private TimingWheel _nextWheel;

        public int TaskCount => 0;

        public bool AddTask( TimeTask timeTask ) { return false; }

        public TimingWheel( long tickSpan, int slotCount, long startMs ) {
            _tickSpan = tickSpan;
            _slotCount = slotCount;
            
        }

        public static void Build( int a ) { }

        private void CreateNextWheel() {
            if( _nextWheel != null ) {
                return;
            }

        }

    }

}
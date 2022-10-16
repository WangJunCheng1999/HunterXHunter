using GameFramework.Extensions;


namespace Framework.TimerTask {

    public class TimeSlot {

        public AtomicInt TaskCount { get; set; }

        public bool RemoveTask( TimeTask timeTask ) { return true; }

    }

}
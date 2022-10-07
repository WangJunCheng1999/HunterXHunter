using System;
using System.Collections.Generic;

namespace Core.TimerTask {
    internal interface ITimeTask {
        public long TimeStamp { get; set; }

    }

    internal class Task {
        public long TriggerStamp { get; set; }
        public Action action { get; set; }
    }
}
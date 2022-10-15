using System;
using System.Text;

namespace GameFramework.TimerTask {

    internal class Task : IEquatable<Task> {

        public long TriggerStamp { get; set; }
        public long Interval { get; set; }
        public bool IsRecycle { get; set; }
        public Action Action { get; set; }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"触发时间:{TriggerStamp},时间间隔:{Interval},循环任务:{IsRecycle},回调:{Action}");
            return sb.ToString();
        }
        
        public bool Equals(long timeStamp, Action action) {
            if (ReferenceEquals(null, action)) return false;
            return TriggerStamp == timeStamp && Equals(Action, action);
        }

        public bool Equals(Task other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TriggerStamp == other.TriggerStamp && Equals(Action, other.Action);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Task)obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(TriggerStamp, Action);
        }

    }

}
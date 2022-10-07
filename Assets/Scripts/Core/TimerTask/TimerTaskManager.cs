using System;
using System.Collections.Generic;
using Core.TimerTask;
using UnityEngine.Windows.WebCam;

namespace Core.TimerTask {
    internal class TimerTaskManager {
        private readonly Dictionary<long, HashSet<Action>> _actions = new();

        public IEnumerable<Task> GetTasks(long timeStamp) {
            return new List<Task>();
        }
    }
}
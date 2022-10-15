using System;
using System.Collections.Generic;

namespace GameFramework.TimerTask {

    internal class TimerTaskManager {

        private readonly Dictionary<long, List<Task>> _actions = new();

        public bool IsExist(long timeStamp, Action action) {
            foreach (KeyValuePair<long, List<Task>> VARIABLE in _actions) {
                
            }

            return false;
        }

        public void AddTask(long timeStamp, Action action) {

        }


        public IEnumerable<Task> GetTasks(long timeStamp) {
            return new List<Task>();
        }

    }

}
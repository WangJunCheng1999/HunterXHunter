using System;
using System.Collections.Generic;
using GameFramework.Extensions;
using UnityEngine.WSA;


namespace Framework.TimerTask
{
    public class TimeSlot
    {
        private readonly Queue<TimeTask> _timeTasks = new Queue<TimeTask>();
        public AtomicInt TaskCount { get; set; } = new AtomicInt();

        public bool AddTask(TimeTask timeTask)
        {
            TaskCount.Increment();
            _timeTasks.Enqueue(timeTask);
            return true;
        }

        public void Trigger(Action<TimeTask> action)
        {
            while (_timeTasks.Count != 0)
            {
                action?.Invoke(_timeTasks.Dequeue());
                TaskCount.Decrement();
            }
        }

        public bool RemoveTask(TimeTask timeTask)
        {
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Extensions;
using GameFramework.Helper;
using UnityEngine.WSA;


namespace Framework.TimerTask {

    public class TimeSlot {

        // 任务列表
        private readonly LinkedList<TimeTask> _timeTasks = new();

        // 任务数
        private readonly AtomicInt _taskCount;

        // 所属时间轮
        private TimingWheel _timingWheel;

        public TimeSlot( TimingWheel timingWheel, AtomicInt taskCount ) {
            _taskCount = taskCount;
            _timingWheel = timingWheel;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="timeTask"></param>
        /// <returns></returns>
        public bool AddTask( TimeTask timeTask ) {
            _taskCount.Increment();
            _timeTasks.AddLast(timeTask);
            timeTask.TimeSlot = this;
            return true;
        }

        /// <summary>
        /// 触发槽
        /// </summary>
        /// <param name="action"></param>
        public void Trigger( Action<TimeTask> action ) {
            while( _timeTasks.Count != 0 ) {
                var timeTask = _timeTasks.First;
                _timeTasks.RemoveFirst();
                action?.Invoke(timeTask.Value);
                _taskCount.Decrement();
            }
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="timeTask"></param>
        /// <returns></returns>
        public bool RemoveTask( TimeTask timeTask ) {
            if( timeTask.TaskStatus != TimeTaskStatus.Wait ) {
                return false;
            }

            var current = _timeTasks.First;

            while( current != null ) {
                if( timeTask == current.Value ) {
                    _timeTasks.Remove(current);
                }

                current = current.Next;
            }

            return true;
        }

    }

}
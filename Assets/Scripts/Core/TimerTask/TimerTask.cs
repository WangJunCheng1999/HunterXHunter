using System;
using System.Collections.Generic;

namespace Core.TimerTask {
    public class TimerTask : Component {
        private readonly TimeWheel _timeWheel = new();
        private readonly TimerTaskManager _taskManager = new();


        /// <summary>
        /// 开启定时任务
        /// </summary>
        public void StartScheduleTask(long timeStamp, Action action) {
        }

        /// <summary>
        /// 暂停定时任务
        /// </summary>
        public void StopScheduleTask(long timeStamp, Action action) {
        }


        /// <summary>
        /// 倒计时任务
        /// </summary>
        public void StartCountDownTask() {
        }

        /// <summary>
        /// 暂停倒计时任务
        /// </summary>
        public void StopCountDownTask() {
        }

        /// <summary>
        /// 循环任务
        /// </summary>
        public void StartCycleTask() {
        }

        /// <summary>
        /// 暂停循环任务
        /// </summary>
        public void StopCycleTask() {
        }

        private void StartTask(Task task) {
        }


        private void StopTask(Task task) {
        }

        public override void OnFixedUpdate(int deltaTime) {
            IEnumerable<long> timeStamps = _timeWheel.Revolve(deltaTime);
            foreach (long timeStamp in timeStamps) {
                TriggerTask(timeStamp);
            }
        }

        /// <summary>
        /// 触发任务
        /// </summary>
        /// <param name="timeStamp"> 时间戳 </param>
        private void TriggerTask(long timeStamp) {
            var tasks = _taskManager.GetTasks(timeStamp);
            foreach (Task task in tasks) {
                task.action?.Invoke();
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace GameFramework.TimerTask {

    public class TimerTask : Component {

        // 时间轮
        private readonly TimeWheel _timeWheel = new();

        // 任务管理器
        private readonly TimerTaskManager _taskManager = new();

        /// <summary>
        /// 开启定时任务
        /// </summary>
        public void StartScheduleTask(long timeStamp, Action action, string name = "default") {
            long interval = Game.TimeStamp - timeStamp;
            // 检查时间戳是否正常
            if (interval < 0) {
                Game.Error($"传入的时间{timeStamp}小于游戏当前时间:{Game.TimeStamp}");
                return;
            }

            // 检查是否已经存在
            if (_taskManager.IsExist(timeStamp, action)) {
                Game.Error($"重复注册时间戳:{timeStamp},name:{name}");
                return;
            }

            _timeWheel.AddFragment(interval);
        }

        /// <summary>
        /// 暂停定时任务
        /// </summary>
        public void StopScheduleTask(long timeStamp, Action action) { }

        /// <summary>
        /// 倒计时任务
        /// </summary>
        public void StartCountDownTask(long countDown, Action action) { }

        /// <summary>
        /// 暂停倒计时任务
        /// </summary>
        public void StopCountDownTask(long countDown, Action action) { }

        /// <summary>
        /// 循环任务    
        /// </summary>
        public void StartCycleTask(long countDown, Action action) { }

        /// <summary>
        /// 暂停循环任务
        /// </summary>
        public void StopCycleTask() { }

        private void StartTask(Task task) { }

        private void StopTask(Task task) { }

        private void StartCycleTask(Task task) { }

        private void StopCycleTask(Task task) { }

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
                if (task.IsRecycle) {
                    StartCycleTask(task.Interval, task.Action);
                }

                task.Action?.Invoke();
            }
        }

    }

}
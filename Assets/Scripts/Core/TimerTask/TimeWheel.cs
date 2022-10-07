using System.Collections.Generic;

namespace Core.TimerTask {
    internal class TimeWheel {
        private readonly List<TimeFragment> _fragments;
        private readonly int _fragNum;
        private readonly int _interval;
        private long _leastUpdateTime;

        public TimeWheel(int interval = 10, int fragNum = 10) {
            _interval = interval;
            _fragNum = fragNum;
            _fragments = new List<TimeFragment>(fragNum);
            _leastUpdateTime = 0;

            for (int i = 0; i < _fragments.Count; i++) {
                _fragments.Add(new TimeFragment(interval));
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="delay"></param>
        public void AddFragment(long delay) {
            long current = delay % (_fragNum * _interval);
            current /= _interval;
            _fragments[(int)current].Add(delay);
        }

        public void RemoveFragment(long timeStamp) {
        }


        public IEnumerable<long> Revolve(int deltaTime) {
            CheckFragments(deltaTime);
            _leastUpdateTime += deltaTime;
            return new Queue<long>();
        }


        public List<long> GetTriggerTimestamp(long timeStamp) {
            return null;
        }

        private void CheckFragments(long deltaTime) {
            for (int i = 0; deltaTime > 0; i = (i + 1) % _fragNum) {
                deltaTime -= _interval;
            }
        }

        private long GetNextInterval() {
            return 10000;
        }
    }
}
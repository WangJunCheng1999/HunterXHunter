using System;


namespace GameFramework.Helper {

    public interface IDateTimeHelper {

        public long GetTimestamp( bool isSecond = false );

        public long GetTimestamp( DateTime date, bool isSecond = false );

    }

    public class DateTimeHelper : IDateTimeHelper {

        /// <summary>
        /// 获取当前时间戳，默认毫秒级
        /// </summary>
        /// <param name="isSecond">是否秒级时间戳</param>
        /// <returns></returns>
        public long GetTimestamp( bool isSecond = false ) { return GetTimestamp(DateTime.Now, isSecond); }

        /// <summary>
        /// 获取指定时间戳，默认毫秒级
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="isSecond">是否秒级时间戳</param>
        /// <returns></returns>
        public long GetTimestamp( DateTime date, bool isSecond = false ) {
            var dateTimeOffset = new DateTimeOffset(date);
            return isSecond ? dateTimeOffset.ToUnixTimeSeconds() : dateTimeOffset.ToUnixTimeMilliseconds();
        }

    }

}
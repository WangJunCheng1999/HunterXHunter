using System;
using System.Diagnostics;
using GameFramework.Helper;
using GameFramework.Logger;


namespace GameFramework {

    public static class Game {

        public static void Start() { }

        public static void Update(float deltaTime) { }

        public static void FixedUpdate(float deltaTime) { }

        public static void LateUpdate(float deltaTime) { }


#region 日志

        public static ILoggerHelper Logger;

        /// <summary>
        /// 带标签的普通日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Log( string tag, object obj ) { Logger.Log(tag, obj); }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="obj"></param>
        public static void Log( object obj ) { Logger.Log(obj); }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="obj"></param>
        public static void Warning( object obj ) { Logger.Warning(obj); }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="obj"></param>
        public static void Error( object obj ) { Logger.Error(obj); }

#endregion


#region 时间戳

        private static readonly IDateTimeHelper TimeHelper = new DateTimeHelper();

        /// <summary>
        /// 秒级时间
        /// </summary>
        public static long TimeStamp => TimeHelper.GetTimestamp(true);

        /// <summary>
        /// 毫秒级时间
        /// </summary>
        public static long TimeStampMillisecond => TimeHelper.GetTimestamp();

#endregion


    }

}
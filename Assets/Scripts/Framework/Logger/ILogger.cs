namespace GameFramework.Logger {

    public interface ILoggerHelper {

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="obj"></param>
        public void Log( object obj );

        /// <summary>
        /// 带标签的普通日志
        /// </summary>
        /// <param name="obj"></param>
        public void Log( string tag, object obj );

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="obj"></param>
        public void Warning( object obj );

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="obj"></param>
        public void Error( object obj );

    }

}
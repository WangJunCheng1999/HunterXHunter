namespace GameFramework.Logger {

    public interface ILogger {

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="obj"></param>
        public void Log(object obj);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="obj"></param>
        public void Warning(object obj);

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="obj"></param>
        public void Error(object obj);

    }

}
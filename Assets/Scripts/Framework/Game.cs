using System;
using System.Diagnostics;
using GameFramework.Logger;

namespace GameFramework {

    public static class Game {

        private static ILogger _logger;

        public static ILogger Logger {
            set => _logger ??= value;
        }
        internal static void Log(object obj) { }
        internal static void Warning(object obj) { }
        internal static void Error(object obj) { }

        public static long TimeStamp =>( DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).Milliseconds;

    }

}
using System.Text;
using Framework.TimerTask;
using GameFramework.Logger;
using UnityEngine;


namespace Logger {

    public class LoggerHelper : ILoggerHelper {

        public void Log( object obj ) { Debug.Log($"{obj}"); }

        public void Log( string tag, object obj ) {
            StringBuilder sb = new StringBuilder();
            sb.Append("#");
            sb.Append(tag);
            sb.Append("# ");
            sb.Append(obj);
            Debug.Log(sb);
        }

        public void Warning( object obj ) {
            Debug.LogWarning(obj);
        }

        public void Error( object obj ) {
            Debug.LogError(obj);
        }

    }

}
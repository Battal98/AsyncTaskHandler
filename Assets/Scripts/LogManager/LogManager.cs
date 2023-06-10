using UnityEngine;

namespace Managers
{
    public static class LogManager
    {
        private static string _prefix;
        private const string Suffix = "</color>";
        private const string WhitePrefix = "<color=white>";
        private const string YellowPrefix = "<color=yellow>";
        private const string RedPrefix = "<color=red>";

        
        /// <summary>
        /// Log to console respect by log type and log name
        /// </summary>
        /// <param name="message">Logging message</param>
        /// <param name="logType">Type of Log</param>
        /// <param name="logName">Name of Log</param>
        public static void Log(string message, LoggerType logType, LoggerName logName)
        {
            // Load logs data
            LogData _logData = Resources.Load<LogData>("LogEnumData");

            if (_logData == null)
            {
                Debug.LogError("CAN'T FOUND LOG DATA ASSET.");
                return;
            }
            
            // Get log type Color
            Color color = _logData.GetColor(logName);

            // Convert color to html
            string _prefix = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";

            // print
            switch (logType)
            {
                case LoggerType.Log:
                    Debug.Log($"{_prefix}{logName}: {Suffix}" +
                              $"{WhitePrefix}{message}{Suffix}");
                    break;
                
                case LoggerType.Warning:
                    Debug.LogWarning($"{_prefix}{logName}: {Suffix}" +
                                     $"{YellowPrefix}{message}{Suffix}");
                    break;
                
                case LoggerType.Error:
                    Debug.LogError($"{_prefix}{logName}: {Suffix}" +
                                   $"{RedPrefix}{message}{Suffix}");
                    break;
            }
        }
    }
}
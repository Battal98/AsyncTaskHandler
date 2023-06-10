using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum LoggerName
    {
        Test,
        Player,
        Manager,
        Time,
        ActionTask,
        Input,
    }

    public enum LoggerType
    {
        Log,
        Warning,
        Error
    }

    [CreateAssetMenu(fileName = "LogData", menuName = "Data/LogData", order = 1)]
    public class LogData : ScriptableObject
    {
        [SerializeField]
        private LogTypeDictionary<LoggerName, Color> _dictionary = new LogTypeDictionary<LoggerName, Color>();

        public Color GetColor(LoggerName logName)
        {
            if (_dictionary.ContainsKey(logName))
            {
                return _dictionary[logName];
            }

            Debug.LogWarning($"The given unit type {logName} does not have a corresponding color value.");
            return Color.white;
        }

        public void SetColor(LoggerName logName, Color color)
        {
            _dictionary.Add(logName, color);
        }
    }

    [Serializable]
    public class LogTypeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                this[keys[i]] = values[i];
            }
        }
    }
}
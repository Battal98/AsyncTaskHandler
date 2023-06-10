using NaughtyAttributes;
using UnityEngine;

namespace AsyncHandler
{
    public class TaskInvoker : MonoBehaviour
    {
        [Button]
        public async void Invoke()
        {
            await AsyncEventsHandler.TaskOne?.Invoke();
        }
    } 
}

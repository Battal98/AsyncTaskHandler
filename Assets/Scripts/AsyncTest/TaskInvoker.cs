using NaughtyAttributes;
using System.Threading.Tasks;
using UnityEngine;

namespace AsyncHandler
{
    public class TaskInvoker : MonoBehaviour
    {
        #if UNITY_EDITOR

        [Button]
        public async void Invoke()
        {
            await AsyncEventsHandler.TaskOne?.Invoke();
        }

        [Button]
        private void Insert()
        {
            AsyncEventsHandler.TaskOne.Insert(1, InsertTest);
        }
        private async Task InsertTest()
        {
            Debug.Log("Insert is Worked!");
            await Task.Delay(1000);
        } 

        #endif
    } 
}

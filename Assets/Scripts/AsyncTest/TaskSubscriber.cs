using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace AsyncHandler
{
    public class TaskSubscriber : MonoBehaviour
    {
        [SerializeField]
        private int order = 0;
        [SerializeField]
        private int delay = 1;

        private const int DelayMultiplier = 1000;

        private void OnEnable()
        {
            AsyncEventsHandler.TaskOne.Subscription(isSubscribe: true, order: order, action: TaskOneSubscriber,isImportant: false);
        }

        private void OnDisable()
        {
            AsyncEventsHandler.TaskOne.Subscription(false, order, TaskOneSubscriber, false);

        }

        private async Task TaskOneSubscriber()
        {
            this.transform.DOMoveZ(this.transform.position.z + 1f, delay);
            await Task.Delay(DelayMultiplier * delay);

        }
    } 
}

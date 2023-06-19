using DG.Tweening;
using NaughtyAttributes;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace AsyncHandler
{
    public class TaskInvoker : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro isInsertedText;
        [SerializeField]
        private TextMeshPro orderText;
        [SerializeField]
        private GameObject obj;
        [SerializeField]
        private int newOrder = 0;

        private void Awake()
        {
            isInsertedText.text = "IsInserted = false";
            isInsertedText.color = Color.red;
            orderText.text = "Order: None";
        }

#if UNITY_EDITOR

        [Button]
        public async void Invoke()
        {
            await AsyncEventsHandler.TaskOne?.Invoke();
        }

        [Button]
        private void Insert()
        {
            AsyncEventsHandler.TaskOne.Insert(newOrder, InsertTest);
            isInsertedText.text = "IsInserted = true";
            isInsertedText.color = Color.green;
            orderText.text = $"Order: {newOrder}";
        }
        private async Task InsertTest()
        {
            obj.transform.DOLocalMoveZ(obj.transform.localPosition.z + 1f, 1f);
            await Task.Delay(1000);
        } 

        #endif
    } 
}

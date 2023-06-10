using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// Author : Battal Yigit PATLAR - @Battal98 || Hakan MELEN - @melenglobal

namespace AsyncHandler
{
    public class AsyncActionHandler
    {
        #region Self Variables

        private SortedList<int, TaskListener> listeners = new ();
        private CancellationTokenSource cancellationTokenSource;
        
        #endregion

        #region Subscribe Jobs

        private void CreateNewOrder(int order = 0, bool isImportant = false)
        {
            if (listeners.ContainsKey(order)) return;
            listeners.Add(order, new TaskListener(isImportant));
        }
        
        private void RemoveOrder(int order = 0)
        {
            if (!listeners.ContainsKey(order)) return;
            if (listeners[order].Count > 0) return;
            listeners.Remove(order);
        }
        
        public void Subscription(bool isSubscribe, int order, Func<Task> action, bool isImportant = false)
        {
            if (isSubscribe)
            {
                CreateNewOrder(order, isImportant);
                listeners[order].Add(action);
            }
            else
            {
                if (!listeners.ContainsKey(order))
                    return;
            
                listeners[order].Remove(action);
                RemoveOrder(order);
            }
        }
        
        private async Task RetryTaskAsync(Task task, string taskName, CancellationToken cancellationToken,bool isImportant)
        {
            const int maxRetryAttempts = 3;
            const int delayBetweenRetries = 5000; // milliseconds

            int retryCount = 0;
            bool success = false;

            while (!success && retryCount < maxRetryAttempts)
            {
                try
                {
                    await task;
                    //TODO Add Script name to this Yigit
                    success = true;
                }
                catch (Exception ex)
                {
                    // Log the retry attempt with the task name
                    Debug.LogError($"Task Retry Failed for '{taskName}': {ex.Message} : IMPORTANT :{isImportant}");
                    retryCount++;
                    var delay = delayBetweenRetries * retryCount;
                    await Task.Delay(delay, cancellationToken);
                }
            }
            
            if (!success)
            {
                if (isImportant)
                {
                    Debug.Log($"Task Retry Failed for {taskName}: IMPORTANT: {isImportant}");
                    StopAllTasks();
                    //TODO: Return the main menu or something
                }
            }
        }
        
        public void ClearSubscriptions()
        {
            if (listeners.Count <= 0 )
                return;
            listeners.Clear();
            listeners.TrimExcess();
        }

        #endregion

        #region Invoke Jobs
        
        public async Task Invoke()
        {
            if (listeners.Count <= 0) 
            {
                Debug.LogWarning("No TaskListener or subscriber");
                return;
            }
            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            try
            {
                string taskName = null;
                // Diger islemler burada yapilir
                foreach (var order in listeners)
                {
                    List<Task> tasks = new List<Task>(0);
                    
                    foreach (var handler in order.Value)
                    {
                        taskName = handler.GetMethodName(); // Replace GetTaskName with your logic to obtain the task name
                        tasks.Add(RetryTaskAsync(handler(), taskName, cancellationTokenSource.Token,order.Value.IsImportant));
                    }
                    await Task.WhenAll(tasks);
                    
                    cancellationToken.ThrowIfCancellationRequested();
                }
                Debug.Log($" Finished! - NO ERRORS ");
                //break
            }
            catch (OperationCanceledException)
            {
                // Oyun iptal edildi
                Debug.LogWarning($" Action Task Canceled!");
                throw; // iptal durumunu yukari dogru iletmek icin yeniden firlatiliyoruz.
            }
            
            finally
            {
                cancellationTokenSource.Dispose();
            }   
        }

        #endregion
        
        public void StopAllTasks()
        {
            // Oyunu durdurmak icin CancellationTokenSource'i iptal et
            cancellationTokenSource?.Cancel();
        }
    }
}

# AsyncTaskHandler

"I present to you a system in which certain events are connected like an Event system, 
and event sequences are executed in a specific order, deciding whether to continue or not based on the importance level of the given 'Error' situations,
and where you can add a new process with a function in between. It is not only easy to use but also not difficult to understand. 
If you add innovations or make any comments, we can improve the system even further."

## Test Invoker in Hierarchy 
![Invoker_SS](https://github.com/Battal98/AsyncTaskHandler/assets/68375602/e65eb6f9-3c3f-4fd5-b253-3a057d6a8e42)

## How is Subscribe Tasks?
```c#
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
  ```
 ## How is Create Async Task Action?
 ```c#
	public static class AsyncEventsHandler
	{
		public static AsyncActionHandler TaskOne = new AsyncActionHandler();
	} 
 ```

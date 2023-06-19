# AsyncTaskHandler

"I present to you a system in which certain events are connected like an Event system, 
and event sequences are executed in a specific order, deciding whether to continue or not based on the importance level of the given 'Error' situations,
and where you can add a new process with a function in between. It is not only easy to use but also not difficult to understand. 
If you add innovations or make any comments, we can improve the system even further."

## Test Invoker in Hierarchy 
![Invoker_SS](https://github.com/Battal98/AsyncTaskHandler/assets/68375602/e65eb6f9-3c3f-4fd5-b253-3a057d6a8e42)

 ## How is Create Async Task Action?
 ```c#
	public static class AsyncEventsHandler
	{
		public static AsyncActionHandler TaskOne = new AsyncActionHandler();
	} 
 ```
## How is Subscribe Tasks?
The important thing to note is that the functions we subscribe to should be `<async Task>`  functions. The subscription system does not work for functions that are `<void>` .
Dotween has been used for visual examples.
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
	    // its uses DoTween for 
            this.transform.DOMoveZ(this.transform.position.z + 1f, delay);
            await Task.Delay(DelayMultiplier * delay);

        }
```
## How is Insert Tasks?
```c#
        private void Insert()
        {
            AsyncEventsHandler.TaskOne.Insert(newOrder, InsertTest);
	}
```
## How is it lookslike?
![Movie_001](https://github.com/Battal98/AsyncTaskHandler/assets/68375602/a6d18c52-ccae-4d28-a151-656765b5dc3e)

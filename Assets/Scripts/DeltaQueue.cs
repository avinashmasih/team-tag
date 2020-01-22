using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DeltaQueue : MonoBehaviour
{
    [System.Serializable]
    public struct DeltaQueueItem
    {
        [SerializeField]
        public float time;
        [SerializeField]
        public string item;

        public DeltaQueueItem(float time, string item) {
            this.time = time;
            this.item = item;
        }
    }

    Queue<DeltaQueueItem> queue = new Queue<DeltaQueueItem>();

    [Tooltip("The items that the player(s) should draw")]
    public List<string> items;

    public float timePerItem = 5.0f;
    public float delayBetweenItems = 1.5f;

    [System.Serializable]
    public class ClockAdvanced : UnityEvent<string> { }
    public ClockAdvanced OnClockAdvanced;

    [System.Serializable]
    public class ClockComplete : UnityEvent { }
    public ClockComplete OnClockCompleted;

    public delegate void SecondElapsed(float timeRemaining);
    public event SecondElapsed OnSecondElapsed;

    public delegate void WaitTimeBegan();
    public event WaitTimeBegan OnWaitTimeBegan;

    public delegate void WaitTimeEnded();
    public event WaitTimeEnded OnWaitTimeEnded;

    private float untilNext = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in items)
        {
            queue.Enqueue(new DeltaQueueItem(timePerItem, item));
        }
        Invoke("MoveOn", 0);
        InvokeRepeating("Tick", 0, 1);
        untilNext = timePerItem;
    }

    void Wait()
    {
        OnWaitTimeBegan?.Invoke();
        Invoke("MoveOn", delayBetweenItems);
    }

    void MoveOn()
    {
        OnWaitTimeEnded?.Invoke();
        var current = queue.Dequeue();
        Invoke(queue.Count != 0 ? "Wait" : "Finish", current.time);
        OnClockAdvanced?.Invoke(current.item);
        untilNext = current.time;
    }

    void Finish()
    {
        Debug.Log("All done!");
        CancelInvoke("Tick");
        OnClockCompleted?.Invoke();
    }

    void Tick()
    {
        OnSecondElapsed?.Invoke(untilNext--);
    }
}

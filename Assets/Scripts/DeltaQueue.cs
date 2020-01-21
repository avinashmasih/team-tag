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

    public List<string> items;

    public float timePerItem = 5.0f;
    public float delayBetweenItems = 1.5f;

    [System.Serializable]
    public class ClockAdvanced : UnityEvent<string> { }
    public ClockAdvanced OnClockAdvanced;

    [System.Serializable]
    public class ClockComplete : UnityEvent { }
    public ClockComplete OnClockCompleted;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in items)
        {
            queue.Enqueue(new DeltaQueueItem(timePerItem, item));
        }
        Invoke("MoveOn", 0);
    }

    void MoveOn()
    {
        var current = queue.Dequeue();
        Debug.Log($"Draw a {current.item}");
        Invoke(queue.Count != 0 ? "MoveOn" : "Finish", current.time);
        OnClockAdvanced?.Invoke(current.item);
    }

    void Finish()
    {
        Debug.Log("All done!");
        OnClockCompleted?.Invoke();
    }
}

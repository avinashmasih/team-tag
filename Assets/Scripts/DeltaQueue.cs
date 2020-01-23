using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeltaQueue : MonoBehaviour
{    
    Queue<string> queue = new Queue<string>();

    [Tooltip("The items that the player(s) should draw")]
    public List<string> items;

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
            queue.Enqueue(item);
        }
        MoveOn();
    }

    public void MoveOn()
    {
        if (queue.Count == 0)
        {
            Finish();
            return;
        }
        var current = queue.Dequeue();
        OnClockAdvanced?.Invoke(current);
    }

    public void Finish()
    {
        OnClockCompleted?.Invoke();
    }
}

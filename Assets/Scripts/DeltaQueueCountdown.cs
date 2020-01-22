using UnityEngine;
using UnityEngine.UI;

public class DeltaQueueCountdown : MonoBehaviour
{
    public Text countdown;

    public DeltaQueue queue;

    private void Start()
    {
        queue.OnSecondElapsed += Tick;
    }

    void Tick(float timeRemaining)
    {
        if (!countdown) return;
        int minutesLeft = (int)timeRemaining / 60;
        int secondsLeft = (int)timeRemaining % 60;
        countdown.text = minutesLeft <= 0 ?
            secondsLeft.ToString() :
            $"{minutesLeft}:{secondsLeft}";
    }
}
